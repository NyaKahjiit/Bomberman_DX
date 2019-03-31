using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateBomb : MonoBehaviour
{
    public static int limit = 1;
    public GameObject Bomb;
    private Vector3 Planned;
    public static List<Vector3> BombsList = new List<Vector3>();
    public static int limBombs=1;
    private GameObject tBomb;
    const float hightBomb = 0.25f;
    const int standSizeBlock = 1;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<Animator>().SetTrigger("setbomb");
            DelayAnimationPlaneBomb();
            PlanBomb();
        }
    }
    IEnumerator DelayAnimationPlaneBomb()
    {
        CoroutineController.isCoroutine = true;
        yield return new WaitForSeconds(2.22f);
        PlanBomb();
        CoroutineController.isCoroutine = false;
    }
    void PlanBomb()
    {
        Planned = transform.position;
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
        double RaschetnX = Planned.x / (standSizeBlock + InitiatedZone.spaceBlock);
        RaschetnX = Math.Round(RaschetnX, MidpointRounding.ToEven);
        RaschetnX = Math.Round(RaschetnX * (standSizeBlock + InitiatedZone.spaceBlock), 2, MidpointRounding.AwayFromZero);
        double RaschetnZ = Planned.z / (standSizeBlock + InitiatedZone.spaceBlock);
        RaschetnZ = Math.Round(RaschetnZ, MidpointRounding.AwayFromZero);
        RaschetnZ = Math.Round(RaschetnZ * (standSizeBlock + InitiatedZone.spaceBlock), 2, MidpointRounding.AwayFromZero);
        Vector3 centerCell = new Vector3((float)RaschetnX, hightBomb, (float)RaschetnZ);
        return centerCell;
    }
}
