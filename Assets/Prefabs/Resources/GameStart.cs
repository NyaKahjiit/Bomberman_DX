using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public int x_count = 11;
    public int y_count = 11;
    void Start()
    {
        InitZone zo = new InitZone();
        zo.Start();
        return;
    }

    void Update()
    { 

    }
}
