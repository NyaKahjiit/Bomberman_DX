using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateBomb : MonoBehaviour
{
    public GameObject Bomb;
    private Vector3 Planned;
    public static List<Vector3> BombsList = new List<Vector3>();
    public int limBombs;
    private GameObject tBomb;
    const float hightBomb = 0.25f;
    const int standSizeBlock = 1;

    void Update()
    {
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
        RaschetnX = Math.Round(RaschetnX, MidpointRounding.ToEven);
        RaschetnX = Math.Round(RaschetnX * (standSizeBlock + InitZone.SpaceBlock), 2, MidpointRounding.AwayFromZero);
        double RaschetnZ = Planned.z / (standSizeBlock + InitZone.SpaceBlock);
        RaschetnZ = Math.Round(RaschetnZ, MidpointRounding.AwayFromZero);
        RaschetnZ = Math.Round(RaschetnZ * (standSizeBlock + InitZone.SpaceBlock), 2, MidpointRounding.AwayFromZero);
        Vector3 centerCell = new Vector3((float)RaschetnX, hightBomb, (float)RaschetnZ);
        return centerCell;
    }
}
