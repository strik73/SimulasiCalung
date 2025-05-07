using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InspectUI : MonoBehaviour
{
    public Button[] disabledButtons;
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private GameObject tutorialPanel;

    void Start()
    {
        Time.timeScale = 0;
        foreach (Button button in disabledButtons)
        {
            button.interactable = false;
        }
    }
    public void Keluar()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowConfirmationPanel()
    {
        confirmationPanel.SetActive(true);
        foreach (Button button in disabledButtons)
        {
            button.interactable = false;
        }
    }
    public void Tidak()
    {
        confirmationPanel.SetActive(false);
        foreach (Button button in disabledButtons)
        {
            button.interactable = true;
        }
    }

    public void HideTutorialImage()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1;
        foreach (Button button in disabledButtons)
        {
            button.interactable = true;
        }
    }
}
