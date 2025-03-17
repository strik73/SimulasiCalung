using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI components like Text
using TMPro; // Required if using TextMeshPro

public class BermainMode : MonoBehaviour
{
    private SongData currentSong;
    private int currentIndex = 0;
    private bool isFlashing = false;
    public SongManager songmanager;
    public TextMeshProUGUI tapIndicatorText;

    public void StartGame(SongData song)
    {
        if (song == null)
        {
            Debug.LogError("No song selected! Please select a song first.");
            return;
        }

        currentSong = song;
        currentIndex = 0;

        if (currentSong.sequence == null || currentSong.sequence.Length == 0)
        {
            Debug.LogError("The selected song has no sequence data!");
            return;
        }

        ShowNextTap();
    }

    void ShowNextTap()
    {
        if (currentIndex < currentSong.sequence.Length)
        {
            int nextTap = currentSong.sequence[currentIndex];
            tapIndicatorText.text = "Next Tap: " + nextTap;
        }
        else
        {
            // Debug.Log("🎵 Song Finished!");
            // tapIndicatorText.text = "🎵 Song Finished!"; // Optionally update the UI text
            songmanager.ShowPanel();
        }
    }

    public void OnTap(int partIndex)
    {
        if (currentSong == null || currentSong.sequence == null)
        {
            Debug.LogError("No valid song data available!");
            return;
        }

        if (currentIndex < currentSong.sequence.Length)
        {
            int convertedIndex = currentSong.sequence[currentIndex] - 1;
            if (partIndex == convertedIndex)
            {
                // Debug.Log("✅ Correct!");
                currentIndex++;
                ShowNextTap();
            }
            else
            {
                if (!isFlashing)
                {
                    StartCoroutine(FlashRedIndicator());
                }
            }
        }

        IEnumerator FlashRedIndicator()
        {
            isFlashing = true;
            Color originalColor = tapIndicatorText.color;
            tapIndicatorText.color = Color.red;

            yield return new WaitForSeconds(0.3f);

            tapIndicatorText.color = originalColor;
            isFlashing = false;
        }

    }
}