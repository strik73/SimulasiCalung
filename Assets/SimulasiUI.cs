using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulasiUI : MonoBehaviour
{
    private GameObject tutorialPanel;
    private CanvasGroup canvasGroup;
    private float fadeDuration = 0.5f;

    void Start()
    {
        tutorialPanel = GameObject.Find("Panel Tutorial");
        canvasGroup = tutorialPanel.GetComponent<CanvasGroup>();
        tutorialPanel.SetActive(false);
    }
    void Update()
    {
        if (tutorialPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            HideTutorialImage();
        }
    }
    public void Stop()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowTutorialImage()
    {
        tutorialPanel.SetActive(true);
        StartCoroutine(FadeIn());
    }

    public void HideTutorialImage()
    {
        StartCoroutine(FadeOut());
        //tutorialPanel.SetActive(false);
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
