using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public int exploooooosionDelay;
    private Vector3 myCoordinate;
    private void Start()
    {
        myCoordinate = this.gameObject.GetComponent<Transform>().position;
        StartCoroutine(Example());
    }

    public static void Update()
    {
        
    }
    IEnumerator Example()
    {
        yield return new WaitForSeconds(exploooooosionDelay);
        PlayerController.BombsList.Remove(myCoordinate);
        Destroy(this.gameObject);
    }
}
