using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Bomb;
    public int speed, limBombs;
    private Vector3 playvec;
    public static GameObject tBomb;
    public static List<Vector3> BombsList = new List<Vector3>();
    void Update()
    {
        playvec = new Vector3 (Input.GetAxis("Horizontal")*speed, 0, Input.GetAxis("Vertical")* speed);
        this.GetComponent<Rigidbody>().velocity = playvec;
        if (Input.GetKeyDown("space"))
        {
            PlannedBomb();
            BombController.Update();
        }
    }
    void PlannedBomb()
    {
        Vector3 Planned = this.GetComponent<Transform>().position;
        Planned.x = Mathf.Round(Planned.x);
        Planned.z = Mathf.Round(Planned.z);
        if (BombsList.Contains(Planned) | BombsList.Count>=limBombs)
        {
            Debug.Log("Невозможно поставить бомбу");
        }
        else
        {
            Debug.Log("координаты бомбы" + Planned);
            tBomb = Instantiate(Bomb, Planned, Quaternion.identity);
            BombsList.Add(Planned);
        }

    }
}
