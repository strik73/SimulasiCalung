using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulasiUI : MonoBehaviour
{
    public void Stop()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
