using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitZone : MonoBehaviour
{
    public static List<Vector3> EmptyXYMass = new List<Vector3>();
    public static float SpaceBlock;
    public GameObject Playerobj, Field, WallBlock, Box, Err, Enemy;
    public int percentBlock, emptyCellForPlayer;
    private int X_count, Y_count;
    public void Start()
    {
        X_count = PlayerPrefs.GetInt("X_count");
        Y_count = PlayerPrefs.GetInt("Y_count");
        SpaceBlock = PlayerPrefs.GetFloat("distanceBetweenBlocks");
        GeneratedStaticObj();
        GeneratedDynamicObj();
    }
    void GeneratedStaticObj()
    {
        Vector3 EmptyXY = new Vector3();
        Debug.Log("Your setting for field(Cubes): x=" + X_count + ", y=" + Y_count);
        float x_field = 0, y_field = 0;
        GeneratedWalls(ref EmptyXY, X_count, Y_count, ref x_field, ref y_field);
        Field.transform.localScale = new Vector3((x_field + 1) / 10, 1, (y_field + 1) / 10);
        Instantiate(Field, new Vector3((x_field) / 2, 0, (y_field) / 2), Quaternion.identity);
    }
    private void GeneratedWalls(ref Vector3 EmptyXY, int X_count, int Y_count, ref float x_field, ref float y_field)
    {
        float spaceBlockX = 0, spaceBlockY = 0;
        for (int i = 0; i < X_count; i++)
        {
            spaceBlockY = 0;
            for (int j = 0; j < Y_count; j++)
            {
                if (i == 0 | j == 0 | i == X_count - 1 | j == Y_count - 1 | (i % 2 == 0 & j % 2 == 0))
                {
                    Instantiate(WallBlock, new Vector3(i + spaceBlockX, 0.5f, j + spaceBlockY), Quaternion.identity);
                }
                else
                {
                    AddEmptyCoordinate(spaceBlockX,spaceBlockY,i,j);
                }
                if (i == X_count - 1 & j == Y_count - 1)
                {
                    x_field = (i + spaceBlockX);
                    y_field = (j + spaceBlockY);
                }
                spaceBlockY += SpaceBlock;
            }
            spaceBlockX += SpaceBlock;
        }
    }

    private void AddEmptyCoordinate(float spaceBlockX, float spaceBlockY, int i, int j)
    {
        Vector3 EmptyXY = new Vector3(i + spaceBlockX, 0.5f, j + spaceBlockY);
        EmptyXYMass.Add(EmptyXY);
    }

    void GeneratedDynamicObj()
    {
        List<Vector3> playerneiqhbour = new List<Vector3>();
        int playerPosition = Random.Range(0, EmptyXYMass.Count-1);
        Vector3 playerpos = EmptyXYMass[playerPosition];
        Instantiate(Playerobj, EmptyXYMass[playerPosition], Quaternion.identity);
        EmptyXYMass.RemoveAt(playerPosition);
        Vector3 XY_neiqhbour = playerpos;
        for(int i=0; i<emptyCellForPlayer; i++)
        {
            foreach (Vector3 vec in EmptyXYMass)
            {
                if (IsNeibhourCell(XY_neiqhbour, vec))
                {
                    playerneiqhbour.Add(vec);
                }
            }
            XY_neiqhbour = playerneiqhbour[Random.Range(0, playerneiqhbour.Count)];
            EmptyXYMass.Remove(XY_neiqhbour);
            playerneiqhbour.Clear();
        }
        int indexEmptyCellForEnemy = Random.Range(0, EmptyXYMass.Count - 1);
        Vector3 coordinateEnemy = EmptyXYMass[indexEmptyCellForEnemy];
        Instantiate(Enemy, coordinateEnemy, Quaternion.identity);
        EmptyXYMass.RemoveAt(indexEmptyCellForEnemy);
        for (int i = 0; i <= EmptyXYMass.Count/100*percentBlock; i++)
        {
            Vector3 coordinateBlock = EmptyXYMass[Random.Range(0, EmptyXYMass.Count - 1)];
            Instantiate(Box, coordinateBlock, Quaternion.identity);
            EmptyXYMass.Remove(coordinateBlock);
        }
        EmptyXYMass.Add(coordinateEnemy);
        Debug.Log("player created on = " + playerpos);
    }

    public static bool IsNeibhourCell(Vector3 XY_neiqhbour, Vector3 vec)
    {
        return (vec == XY_neiqhbour + new Vector3(1 + SpaceBlock, 0, 0)) | (vec == XY_neiqhbour - new Vector3(1 + SpaceBlock, 0, 0))
               | (vec == XY_neiqhbour + new Vector3(0, 0, 1 + SpaceBlock)) | (vec == XY_neiqhbour - new Vector3(0, 0, 1 + SpaceBlock));
    }
}