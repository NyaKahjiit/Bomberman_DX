using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public static float timer;
    private void Start()
    {
        StartCoroutine(Example());
    }

    public static void Update()
    {
        
    }
    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(5);
        print(Time.time);
        Destroy(this.gameObject);
    }
}
