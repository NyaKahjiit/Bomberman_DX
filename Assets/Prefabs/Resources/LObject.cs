using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LObject : MonoBehaviour
{
    public class LZone : LObject
    {
        public GameObject LField()
        {
            GameObject field = Resources.Load("GameZone/Field/Field") as GameObject;
            return field;
        }
        public GameObject LWall()
        {
            GameObject wall = Resources.Load("GameZone/Wall/Cube") as GameObject;
            return wall;
        }
    }
}
