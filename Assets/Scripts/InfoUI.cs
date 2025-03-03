using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoUI : MonoBehaviour
{
    public void ButtonKembali()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
