using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menucontrols : MonoBehaviour
{
    public void PresstoPlay()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitGamePressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }
    
}
