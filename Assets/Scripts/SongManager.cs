using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongManager : MonoBehaviour
{
    public TextAsset jsonFile;
    public List<SongData> songList;
    public Transform contentPanel;
    public GameObject songButtonTemplate;
    public GameObject songSelectionPanel;
    public GameObject scrollView;
    public BermainMode bermainMode; // Reference to BermainMode script

    private SongData selectedSong;

    void Start()
    {
        LoadSongs();
        PopulateSongList();
        FreezeGame();
    }

    void LoadSongs()
    {
        if (jsonFile != null)
        {
            SongList songs = JsonUtility.FromJson<SongList>(jsonFile.text);
            songList = new List<SongData>(songs.songs);
        }
        else
        {
            Debug.LogError("JSON file not assigned");
        }
    }

    void PopulateSongList()
    {
        foreach (SongData song in songList)
        {
            GameObject newButton = Instantiate(songButtonTemplate, contentPanel);
            newButton.SetActive(true);
            newButton.transform.localScale = Vector3.one;

            TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = song.name;
            newButton.GetComponent<Button>().onClick.AddListener(() => SelectSong(song));
        }
    }

    public void SelectSong(SongData song)
    {
        selectedSong = song;
        UnfreezeGame();
        HidePanel();
        bermainMode.StartGame(selectedSong);
    }

    public SongData GetSelectedSong()
    {
        return selectedSong;
    }

    private void FreezeGame()
    {
        Time.timeScale = 0;
    }

    private void UnfreezeGame()
    {
        Time.timeScale = 1;
    }

    private void HidePanel()
    {
        contentPanel.gameObject.SetActive(false);
        scrollView.SetActive(false);
        songSelectionPanel.SetActive(false);
    }
}
