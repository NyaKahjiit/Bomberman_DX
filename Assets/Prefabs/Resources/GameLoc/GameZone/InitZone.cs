using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitZone : MonoBehaviour
{
    List<Vector3> EmptyXYMass = new List<Vector3>();
    public float SpaceBlock;
    public GameObject Playerobj;
    public GameObject Field;
    public GameObject WallBlock;
    public GameObject Box;
    public static GameObject Bomb;
    public GameObject Err;
    public int chancecreatebox;
    public void Start()
    {
        Generated_static_object();
        Generated_dynamic_obj();
    }
    void Generated_static_object()
    {
        Vector3 EmptyXY = new Vector3();
        int X_count = PlayerPrefs.GetInt("X_count");
        int Y_count = PlayerPrefs.GetInt("Y_count");
        Debug.Log("Your setting for field(Cubes): x=" + X_count + ", y=" + Y_count);
        float spaceblockx = 0, spaceblocky = 0, x_field = 0, y_field = 0;
        for (int i = 0; i < X_count; i++)
        {
            spaceblocky = 0;
            for (int j = 0; j < Y_count; j++)
            {
                if (i == 0 | j == 0 | i == X_count - 1 | j == Y_count - 1 | (i % 2 == 0 & j % 2 == 0))
                {
                    Instantiate(WallBlock, new Vector3(i + spaceblockx, 0.5f, j + spaceblocky), Quaternion.identity);
                }
                else
                {
                    EmptyXY = new Vector3(i + spaceblockx, 0.5f, j + spaceblocky);
                    EmptyXYMass.Add(EmptyXY);
                }
                if (i == X_count - 1 & j == Y_count - 1)
                {
                    x_field = (i + spaceblockx);
                    y_field = (j + spaceblocky);
                }
                spaceblocky += SpaceBlock;
            }
            spaceblockx += SpaceBlock;
        }
        GameObject newObject = Instantiate(Field, new Vector3((x_field) / 2, 0, (y_field) / 2), Quaternion.identity) as GameObject;
        newObject.transform.localScale = new Vector3((x_field + 1) / 10, 1, (y_field + 1) / 10);
    }
    void Generated_dynamic_obj()
    {
        List<Vector3> playerneiqhbour = new List<Vector3>();
        int pos = Random.Range(1, EmptyXYMass.Count);
        Vector3 playerpos = EmptyXYMass[pos];
        Instantiate(Playerobj, EmptyXYMass[pos], Quaternion.identity);
        EmptyXYMass.RemoveAt(pos);
        Vector3 XY_neiqhbour = playerpos;
        for(int i=0; i<=2; i++)
        {
            foreach (Vector3 deb in EmptyXYMass)
            {
                if ((deb == XY_neiqhbour + new Vector3(1 + SpaceBlock, 0, 0)) | (deb == XY_neiqhbour - new Vector3(1 + SpaceBlock, 0, 0)) | (deb == XY_neiqhbour + new Vector3(0, 0, 1 + SpaceBlock)) | (deb == XY_neiqhbour - new Vector3(0, 0, 1 + SpaceBlock)))
                {
                    playerneiqhbour.Add(deb);
                }
            }
            XY_neiqhbour = playerneiqhbour[Random.Range(0, playerneiqhbour.Count)];
            EmptyXYMass.Remove(XY_neiqhbour);
            playerneiqhbour.Clear();
        }
        for (int i = 0; i <= EmptyXYMass.Count - 1; i++)
        {
            if (Random.Range(0, 100) <= chancecreatebox)
            {
                Instantiate(Box, EmptyXYMass[i], Quaternion.identity);
            }
        }
        Debug.Log("player created on = " + playerpos);
    }
}