using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PresstoPlay()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitGamePressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }
}
