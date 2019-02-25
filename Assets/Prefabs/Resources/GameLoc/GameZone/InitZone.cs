using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitZone : MonoBehaviour
{
    public float SpaceBlock;
    public GameObject Playerobj;
    public GameObject Field;
    public GameObject WallBlock;
    public GameObject Box;
    public static GameObject Bomb;
    public void Start()
    {
        Random rand = new Random();
        List<Vector3> EmptyXYMass = new List<Vector3>();
        List<Vector3> playerneiqhbour = new List<Vector3>();
        Vector3 EmptyXY = new Vector3();
        int X_count = PlayerPrefs.GetInt("X_count");
        int Y_count = PlayerPrefs.GetInt("Y_count");
        Debug.Log("Your setting for field(Cubes): x="+ X_count+", y="+Y_count);
        float zx = 0, zy = 0, x_field=0, y_field=0;
        for (int i = 0; i < X_count ; i++)
        {
            zy = 0;
            for (int j = 0; j < Y_count; j++)
            {
                if (i == 0 | j == 0 | i == X_count - 1 | j == Y_count - 1 | (i%2==0 & j%2==0))
                {
                    Instantiate(WallBlock, new Vector3(i+zx, 0.5f, j+zy), Quaternion.identity);
                }
                else
                {
                    EmptyXY = new Vector3(i + zx, 0.5f, j + zy);
                    EmptyXYMass.Add(EmptyXY);
                }
                if (i == X_count - 1 & j == Y_count - 1)
                {
                    x_field = (i + zx);
                    y_field = (j + zy);
                }
                zy += SpaceBlock;
            }
            zx += SpaceBlock;
        }
        int pos = Random.Range(1, EmptyXYMass.Count);
        Vector3 playerpos = EmptyXYMass[pos];
        playerneiqhbour.Add(playerpos+new Vector3(1.1f, 0, 0));
        GameObject newObject = Instantiate(Field, new Vector3((x_field)/2 , 0, (y_field)/2), Quaternion.identity) as GameObject;
        newObject.transform.localScale = new Vector3((x_field+1)/10 , 1, (y_field+1) / 10);
        Instantiate(Playerobj , EmptyXYMass[pos], Quaternion.identity);
        EmptyXYMass.RemoveAt(pos);

        for (int i =0; i<=EmptyXYMass.Count-1; i++)
        {
            if(Random.Range(0,100)<=50)
            {
                if (playerneiqhbour[0]!= EmptyXYMass[i])
                {
                    Instantiate(Box, EmptyXYMass[i], Quaternion.identity);
                }
            }
        }
        Debug.Log("player created on = "+playerpos);
    }
}