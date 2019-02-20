using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LObject : MonoBehaviour
{
    public GameObject LZone()
    {
        GameObject zone = Resources.Load("GameZone") as GameObject;
        return zone;
    }
    public GameObject LWall()
    {
        GameObject wall = Resources.Load("Cube") as GameObject;
        return wall;
    }

}
