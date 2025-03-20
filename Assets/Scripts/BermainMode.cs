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
    private string MusicNote;
    private int TapNote;

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

    void MusicNoteString(int index)
    {
        switch (index)
        {
            case 1:
                MusicNote = ".5";
                break;
            case 2:
                MusicNote = ".3";
                break;
            case 3:
                MusicNote = ".2";
                break;
            case 4:
                MusicNote = ".1";
                break;
            case 5:
                MusicNote = "6";
                break;
            case 6:
                MusicNote = "5";
                break;
            case 7:
                MusicNote = "3";
                break;
            case 8:
                MusicNote = "2";
                break;
            case 9:
                MusicNote = "1";
                break;
            case 10:
                MusicNote = "6'";
                break;
            case 11:
                MusicNote = "5'";
                break;
            case 12:
                MusicNote = "3'";
                break;
            case 13:
                MusicNote = "2'";
                break;
            case 14:
                MusicNote = "1'";
                break;
            default:
                break;
        }
    }

    void ShowNextTap()
    {

        if (currentIndex < currentSong.sequence.Length)
        {
            int nextTap = currentSong.sequence[currentIndex];
            MusicNoteString(nextTap);
            tapIndicatorText.text = "Tekan Not: " + MusicNote;
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