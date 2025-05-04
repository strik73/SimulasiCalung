using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BermainUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float fadeDuration = 0.5f;
    public Button[] disabledButtons;
    public SongManager songManager;
    public GameObject tutorialImage;
    public GameObject playArea;
    public GameObject ConfirmationPanel;

    public void Keluar()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void HideTutorialImage()
    {
        tutorialImage.SetActive(false);
        foreach (Button button in disabledButtons)
        {
            button.interactable = true;
        }
        songManager.songSelectionPanel.SetActive(true);
        playArea.SetActive(true);
    }
    public void ShowTutorialImage()
    {
        tutorialImage.SetActive(true);
        foreach (Button button in disabledButtons)
        {
            button.interactable = false;
        }
    }

    public void ShowConfirmationPanel()
    {
        ConfirmationPanel.SetActive(true);
        songManager.songSelectionPanel.SetActive(false);
        playArea.SetActive(false);
        Time.timeScale = 0;
    }
    public void Tidak()
    {
        ConfirmationPanel.SetActive(false);
        songManager.songSelectionPanel.SetActive(true);
        playArea.SetActive(true);
        Time.timeScale = 1;
    }

}
