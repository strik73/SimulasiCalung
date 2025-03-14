using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BermainMode : MonoBehaviour
{
    private SongData currentSong;
    private int currentIndex = 0;
    public SongManager songmanager;

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
            Debug.Log("Next tap: " + currentSong.sequence[currentIndex]);
        }
        else
        {
            Debug.Log("🎵 Song Finished!");
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
                Debug.Log("✅ Correct!");
                currentIndex++;
                ShowNextTap();
            }
            else
            {
                Debug.Log("❌ Wrong part, try again.");
            }
        }
    }
}
