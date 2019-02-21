using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menucontrols : MonoBehaviour
{
    public void PresstoPlay()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }
}
