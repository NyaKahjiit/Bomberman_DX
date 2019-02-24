using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public static float timer;
    public static void Update()
    {
        timer =- Time.deltaTime;
        if(timer==0)
        {
            Debug.Log("bomb explosion");
            PlayerController.BombsList.RemoveAt(0);
            Destroy(PlayerController.tBomb);
        }
    }
}
