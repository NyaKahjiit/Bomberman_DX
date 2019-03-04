using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Configuration : MonoBehaviour
{
    public int X_count, Y_count;
    public float dist;
    public InputField X_countf;
    public InputField Y_countf;
    public InputField Distancef;
    private void Start()
    {
        X_countf.text =PlayerPrefs.GetInt("X_count").ToString();
        Y_countf.text = PlayerPrefs.GetInt("Y_count").ToString();
        Distancef.text = PlayerPrefs.GetFloat("distanceBetweenBlocks").ToString();
    }

    public void INsert_X()
    {
        X_count = int.Parse(X_countf.text);
        PlayerPrefs.SetInt("X_count", X_count);
        Debug.Log(X_count);
    }
    public void INsert_Y()
    {
        Y_count = int.Parse(Y_countf.text);
        PlayerPrefs.SetInt("Y_count", Y_count);
        Debug.Log(Y_count);
    }
    public void InsertDistance()
    {
        dist = float.Parse(Distancef.text);
        Debug.Log(dist);
        PlayerPrefs.SetFloat("distanceBetweenBlocks", dist);
    }
    public static void FullScreenToggle()
    {
        bool isFullScreen = new bool();
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
}
