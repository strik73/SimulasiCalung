using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   public GameObject loadingScreen;
   public Slider progressBar;

   public void Simulasi()
   {
    StartCoroutine(LoadSceneAsync("Simulasi"));
   }

   public void Quit()
   {
    Application.Quit();
   }

   IEnumerator LoadSceneAsync(string sceneName)
   {
      loadingScreen.SetActive(true);

      AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
      operation.allowSceneActivation = false;

      while (operation.progress < 0.9f)
      {
        progressBar.value = operation.progress;
        yield return null;
      }

      progressBar.value = 1f;
      operation.allowSceneActivation = true;
   }
}
