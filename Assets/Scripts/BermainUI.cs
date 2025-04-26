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

    public void Stop()
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

}
