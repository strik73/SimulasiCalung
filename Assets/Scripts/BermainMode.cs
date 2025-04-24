using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BermainMode : MonoBehaviour
{
    private SongData currentSong;
    private int currentIndex = 0;
    private bool isFlashing = false;
    private bool isScoreFlashing = false;
    public SongManager songmanager;
    public TextMeshProUGUI tapIndicatorText;
    public TextMeshProUGUI nextTapIndicatorText;
    private string MusicNote;
    private string MusicNoteAfter;
    private int TapNote;
    public GameObject notePrefab;
    public RectTransform spawnPoint;
    public RectTransform hitPoint;
    public float noteSpeed = 200f;
    public float noteSpacing = 1f;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;


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
        score = 0;
        UpdateScoreUI();
        StartCoroutine(SpawnNotes());
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

    public void OnTap(int partIndex)
    {
        if (currentSong == null || currentSong.sequence == null) return;

        FallingNote[] notes = FindObjectsOfType<FallingNote>();
        bool noteHit = false;

        foreach (var note in notes)
        {
            float distance = Mathf.Abs(note.GetComponent<RectTransform>().anchoredPosition.x - hitPoint.anchoredPosition.x);
            if (note.expectedTapIndex == partIndex && distance < 100f)
            {
                note.MarkAsHit();
                Destroy(note.gameObject);
                currentIndex++;
                score += 100;
                UpdateScoreUI();
                noteHit = true;
                return;
            }
        }

        if (!noteHit && !isFlashing)
        {
            if (score > 0)
            {
                score -= 50;
            }
            else
            {
                score = 0;
            }

            UpdateScoreUI();
            StartCoroutine(FlashRedIndicator());
            StartCoroutine(ScoreFlashRed());
        }
    }


    IEnumerator FlashRedIndicator()
    {
        isFlashing = true;

        FallingNote[] notes = FindObjectsOfType<FallingNote>();
        List<Color> originalColors = new List<Color>();

        foreach (var note in notes)
        {
            TextMeshProUGUI noteText = note.GetComponent<TextMeshProUGUI>();
            originalColors.Add(noteText.color);
            noteText.color = Color.red;
        }

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < notes.Length; i++)
        {
            if (notes[i] != null)
            {
                notes[i].GetComponent<TextMeshProUGUI>().color = originalColors[i];
            }
        }

        isFlashing = false;
    }

    public void MissedNote()
    {
        if (score > 50)
        {
            score -= 50;
        }
        else
        {
            score = 0;
        }

        StartCoroutine(ScoreFlashRed());
        UpdateScoreUI();
    }

    IEnumerator ScoreFlashRed()
    {
        isScoreFlashing = true;
        Color originalColor = scoreText.color;
        scoreText.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        scoreText.color = originalColor;
        isScoreFlashing = false;
    }

    void UpdateScoreUI()
    {
        scoreText.text = $"Skor: {score}";
        finalScoreText.text = $"Skor: {score}";
    }


    IEnumerator SpawnNotes()
    {
        for (int i = 0; i < currentSong.sequence.Length; i++)
        {
            SpawnNote(currentSong.sequence[i]);
            yield return new WaitForSeconds(noteSpacing);
        }

        yield return StartCoroutine(WaitForAllNotesDestroyed());

        songmanager.ShowPanel();
    }

    IEnumerator WaitForAllNotesDestroyed()
    {
        while (true)
        {
            FallingNote[] remainingNotes = FindObjectsOfType<FallingNote>();
            if (remainingNotes.Length == 0)
                break;

            yield return new WaitForSeconds(0.5f);
        }
    }


    void SpawnNote(int noteIndex)
    {
        GameObject noteObj = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
        TextMeshProUGUI noteText = noteObj.GetComponent<TextMeshProUGUI>();
        FallingNote noteScript = noteObj.GetComponent<FallingNote>();

        MusicNoteString(noteIndex);
        noteText.text = MusicNote;

        noteScript.expectedTapIndex = noteIndex - 1;
        noteScript.speed = noteSpeed;
        noteScript.hitX = hitPoint.anchoredPosition.y;
    }

}