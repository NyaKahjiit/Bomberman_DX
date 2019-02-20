using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LObject go = new LObject();
        Instantiate(go.LZone(), new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(go.LWall(), new Vector3(-2f, 0.5f, -2f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    { 

    }
}
