using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public static float timer;
    private void Start()
    {
    }

    public static void Update()
    {
        timer -= Time.deltaTime;
        if (timer ==0)
        {
            Destroy(PlayerController.tBomb);
            Debug.Log("bomb explosion");
            PlayerController.BombsList.RemoveAt(0);
        }
    }
}
