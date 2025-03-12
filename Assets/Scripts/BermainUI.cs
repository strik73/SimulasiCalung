using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BermainUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float fadeDuration = 0.5f;

    void Start()
    {
            }
    void Update()
    {
    }
    public void Stop()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
