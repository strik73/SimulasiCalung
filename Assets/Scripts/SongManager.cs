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
    public GameObject musicPanel;
    public GameObject finsihPanel;
    public GameObject calung;
    public BermainMode bermainMode;
    public bool isGameFrozen;
    private SongData selectedSong;
    public BermainUI bermainUI;

    void Start()
    {
        calung.SetActive(false);
        LoadSongs();
        PopulateSongList();
        bermainUI.ShowTutorialImage();
        Time.timeScale = 0;
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
        bermainMode.StartGame(selectedSong);
        PlayMusic();
    }

    public SongData GetSelectedSong()
    {
        return selectedSong;
    }

    private void FreezeGame()
    {
        StartCoroutine(FreezeGameAfterDelay());
    }

    private IEnumerator FreezeGameAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
        isGameFrozen = true;
    }

    private void UnfreezeGame()
    {
        Time.timeScale = 1;
        calung.SetActive(true);
        isGameFrozen = false;
    }

    private void PlayMusic()
    {
        songSelectionPanel.SetActive(false);
        musicPanel.SetActive(true);
    }
    public void StopPlay()
    {
        calung.SetActive(false);
        songSelectionPanel.SetActive(true);
        musicPanel.SetActive(false);
    }
    public void ShowPanel()
    {
        FreezeGame();
        musicPanel.SetActive(false);
        finsihPanel.SetActive(true);
        bermainMode.HomeButton.SetActive(true);
        bermainMode.StopButton.SetActive(false);
    }

    public void KembaliButton()
    {
        finsihPanel.SetActive(false);
        StopPlay();
    }
}
