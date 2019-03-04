using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public GameObject Bomb;
    public int speed, limBombs;
    private Vector3 playvec;
    private GameObject tBomb;
    public static List<Vector3> BombsList = new List<Vector3>();
    private Vector3 Planned;
    const int standSizeBlock=1;
    const float hightBomb = 0.25f;
    void Update()
    {
        playvec = new Vector3 (Input.GetAxis("Horizontal")*speed, 0, Input.GetAxis("Vertical")* speed);
        transform.LookAt(playvec+transform.position);
        this.GetComponent<Rigidbody>().velocity = playvec;
        if (Input.GetKeyDown("space"))
        {
            PlannedBomb();
        }
    }
    void PlannedBomb()
    {
        Planned = this.GetComponent<Transform>().position;
        Planned.x = Mathf.Round(Planned.x);
        Planned.z = Mathf.Round(Planned.z);
        if (BombsList.Contains(Planned) | BombsList.Count >= limBombs)
        {
            Debug.Log("Невозможно поставить бомбу");
        }
        else
        {
            Vector3 planned = CellCenterCalc(Planned);
            tBomb = Instantiate(Bomb, planned, Quaternion.identity);
            BombsList.Add(CellCenterCalc(planned));
        }
    }
    private Vector3 CellCenterCalc(Vector3 Planned)
    {
        double RaschetnX = Planned.x / (standSizeBlock + InitZone.SpaceBlock);
        RaschetnX = Math.Round(RaschetnX, MidpointRounding.AwayFromZero);
        RaschetnX = Math.Round(RaschetnX * (standSizeBlock + InitZone.SpaceBlock), 2, MidpointRounding.AwayFromZero);
        double RaschetnZ = Planned.z /(standSizeBlock + InitZone.SpaceBlock);
        RaschetnZ = Math.Round(RaschetnZ, MidpointRounding.AwayFromZero);
        RaschetnZ = Math.Round(RaschetnZ * (standSizeBlock + InitZone.SpaceBlock), 2, MidpointRounding.AwayFromZero);
        Vector3 centerCell = new Vector3((float)RaschetnX, hightBomb, (float)RaschetnZ);
        Debug.Log("Raschet= " + centerCell);
        return centerCell;
    }

}
