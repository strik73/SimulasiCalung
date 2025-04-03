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
    public TextMeshProUGUI nextTapIndicatorText;
    private string MusicNote;
    private string MusicNoteAfter;
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
                MusicNote = ".2";
                break;
            case 2:
                MusicNote = ".1";
                break;
            case 3:
                MusicNote = "6";
                break;
            case 4:
                MusicNote = "5";
                break;
            case 5:
                MusicNote = "3";
                break;
            case 6:
                MusicNote = "2";
                break;
            case 7:
                MusicNote = "1";
                break;
            case 8:
                MusicNote = "6'";
                break;
            case 9:
                MusicNote = "5'";
                break;
            case 10:
                MusicNote = "3'";
                break;
            case 11:
                MusicNote = "2'";
                break;
            case 12:
                MusicNote = "1'";
                break;
            case 13:
                MusicNote = "6''";
                break;
            case 14:
                MusicNote = "5''";
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
            string currentNote = MusicNote;

            string nextNote = "";
            if (currentIndex + 1 < currentSong.sequence.Length)
            {
                int nextTapIndex = currentSong.sequence[currentIndex + 1];
                MusicNoteString(nextTapIndex);
                nextNote = MusicNote;
            }

            tapIndicatorText.text = $"Tekan Not: {currentNote}";
            nextTapIndicatorText.text = nextNote != "" ? $"{nextNote}" : "-";
        }
        else
        {
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