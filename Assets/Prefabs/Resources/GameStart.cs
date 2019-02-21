using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public int x_count = 17;
    public int y_count = 17;
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
