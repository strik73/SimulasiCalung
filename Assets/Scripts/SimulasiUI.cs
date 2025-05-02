using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulasiUI : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    public GameObject confirmationPanel;
    private CanvasGroup canvasGroup;
    private float fadeDuration = 0.5f;
    [SerializeField] private MonoBehaviour calungManagerScript;
    [SerializeField] private Button[] disabledButtons;

    void Start()
    {
        canvasGroup = tutorialPanel.GetComponent<CanvasGroup>();
        tutorialPanel.SetActive(false);
        Time.timeScale = 1;
    }
    void Update()
    {
        if (tutorialPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            HideTutorialImage();
        }
    }

    public void ShowTutorialImage()
    {
        tutorialPanel.SetActive(true);
        StartCoroutine(FadeIn());
    }

    public void HideTutorialImage()
    {
        StartCoroutine(FadeOut());
    }
    public void ShowConfirmationPanel()
    {
        confirmationPanel.SetActive(true);
        calungManagerScript.enabled = false;
        foreach (Button button in disabledButtons)
        {
            button.interactable = false;
        }
        Time.timeScale = 0;
    }

    public void Keluar()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Tidak()
    {
        confirmationPanel.SetActive(false);
        calungManagerScript.enabled = true;
        foreach (Button button in disabledButtons)
        {
            button.interactable = true;
        }
        Time.timeScale = 1;
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = elapsedTime / fadeDuration;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = 1 - (elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        tutorialPanel.SetActive(false);
    }

}
