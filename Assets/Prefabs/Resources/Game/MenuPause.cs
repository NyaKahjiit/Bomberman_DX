using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public float timer;
    public bool ispuse;
    public bool guipuse;

    void Update()
    {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false)
        {
            ispuse = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true)
        {
            ispuse = false;
        }
        if (ispuse == true)
        {
            timer = 0;
            guipuse = true;

        }
        else if (ispuse == false)
        {
            timer = 1f;
            guipuse = false;

        }
    }
    public void OnGUI()
    {
        var pauseskin = GUI.skin.GetStyle("Label");
        pauseskin.fontSize = 20;
        pauseskin.alignment = TextAnchor.UpperCenter;
        if (guipuse == true)
        {
            GUI.Label(new Rect((float)(Screen.width/2), (float)(Screen.height/2)-100f, 150f, 45f), "Пауза", pauseskin);
            Cursor.visible = true;// включаем отображение курсора
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 30f, 150f, 45f), "Продолжить"))
            {
                ispuse = false;
                timer = 0;
                Cursor.visible = false;
            }
            {

            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) + 30f,150f, 45f), "В Меню"))
            {
                ispuse = false;
                timer = 0;
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
