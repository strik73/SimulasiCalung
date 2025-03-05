using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InspectUI : MonoBehaviour
{
    public Button[] disabledButtons;

    void Start()
    {
        Time.timeScale = 0;
        foreach (Button button in disabledButtons)
        {
            button.interactable = false;
        }
    }
    public void Stop()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void HideTutorialImage()
    {
        GameObject.Find("TutorialImage").SetActive(false);
        Time.timeScale = 1;
        foreach (Button button in disabledButtons)
        {
            button.interactable = true;
        }
    }
}
