using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitiatedZone : MonoBehaviour
{
    public static List<Vector3> emptyCells = new List<Vector3>();
    public static float spaceBlock;
    public static Vector3 playerPosition;
    public GameObject Playerobj, Field, WallBlock, Box, Err, Enemy, Bonus,map, flagSpeed, flagRad;
    public int percentBlock, emptyCellForPlayer, countStupidEnemys, countBonus;
    private int X_count, Y_count;
    public static int[,] tileMap;
    public Text radiusExplosion, speedUp;
    public void Start()
    {
        X_count = PlayerPrefs.GetInt("X_count");
        Y_count = PlayerPrefs.GetInt("Y_count");
        tileMap = new int[X_count, Y_count];
        spaceBlock = PlayerPrefs.GetFloat("distanceBetweenBlocks");
        CreateTileMap(X_count, Y_count);
        GeneratedStaticObj();
        GenerateDynamicObj();
    }
    private void Update()
    {
        radiusExplosion.text = "Radius Expl Bomb= " + CreateBomb.limit;
        if(CoroutineController.speed==5)
        {
            speedUp.gameObject.SetActive(true);
        }
    }

    void CreateTileMap(int sizeX, int sizeY)
    {
        for (int indexX = 0; indexX < sizeX; indexX++)
        {
            for (int indexY = 0; indexY < sizeY; indexY++)
            {
                if (indexX == 0 | indexY == 0 | indexX == sizeX - 1 | indexY == sizeY - 1 | (indexX % 2 == 0 & indexY % 2 == 0))
                {
                    tileMap[indexX, indexY] = -5;
                }
                else
                {
                    tileMap[indexX, indexY] = 0;
                }
            }
        }
    }

    void GeneratedStaticObj()
    {
        Debug.Log("Your setting for field(Cubes): x=" + X_count + ", y=" + Y_count);
        GenerateWalls();
        Vector3 installField = new Vector3((X_count + (spaceBlock * X_count)), 1, (Y_count + (spaceBlock * Y_count)));
        Field.transform.localScale = new Vector3((installField.x-spaceBlock)/10,1,(installField.z-spaceBlock)/10);
        Instantiate(Field, new Vector3((installField.x-(1+spaceBlock))/2,0,(installField.z-(1+spaceBlock))/2),Quaternion.identity);

        void GenerateWalls()
        {
            float spaceX = 0;
            for (int indexX = 0; indexX < X_count; indexX++)
            {
                float spaceY = 0;
                for (int indexY = 0; indexY < Y_count; indexY++)
                {
                    if (tileMap[indexX, indexY] == -5)
                    {
                        Instantiate(WallBlock, new Vector3(indexX +spaceX, 0.5f, indexY+spaceY), Quaternion.identity);
                    }
                    else
                    {
                        emptyCells.Add(new Vector3(indexX + spaceX, 0f, indexY + spaceY));
                    }
                    spaceY += spaceBlock;
                }
                spaceX += spaceBlock;
            }
        }
    }


    void GenerateDynamicObj()
    {
        List<Vector3> playerneiqhbour = new List<Vector3>();
        List<Vector3> wayForPlayer = new List<Vector3>();
        List<Vector3> pointsEnemySpavn = new List<Vector3>();
        Vector3 playerpos = emptyCells[Random.Range(0, emptyCells.Count - 1)];
        wayForPlayer.Add(playerpos);
        playerPosition = playerpos;
        Instantiate(Playerobj, playerpos, Quaternion.identity);
        emptyCells.Remove(playerpos);
        Vector3 XY_neiqhbour = playerpos;
        for(int i=0; i<emptyCellForPlayer; i++)
        {
            foreach (Vector3 vec in emptyCells)
            {
                if (IsNeibhourCell(XY_neiqhbour, vec))
                {
                    playerneiqhbour.Add(vec);
                }
            }
            XY_neiqhbour = playerneiqhbour[Random.Range(0, playerneiqhbour.Count)];
            wayForPlayer.Add(XY_neiqhbour);
            emptyCells.Remove(XY_neiqhbour);
            playerneiqhbour.Clear();
        }
        for (int i = 0; i < countStupidEnemys; i++)
        {
            int indexEmptyCellForEnemy = Random.Range(0, emptyCells.Count - 1);
            Vector3 coordinateEnemy = emptyCells[indexEmptyCellForEnemy];
            pointsEnemySpavn.Add(coordinateEnemy);
            Instantiate(Enemy, coordinateEnemy, Quaternion.identity);
            emptyCells.RemoveAt(indexEmptyCellForEnemy);
        }
        //for (int i = 0; i != countBonus; i++)
        //{
        //    Vector3 coordinateBonus = emptyCells[Random.Range(0, emptyCells.Count - 1)];
        //    Instantiate(Bonus, coordinateBonus, Quaternion.identity);
        //}
        for (int i = 0; i <= emptyCells.Count * (percentBlock * 0.01f); i++)
        {
            Vector3 coordinateBlock = emptyCells[Random.Range(0, emptyCells.Count - 1)];
            emptyCells.Remove(coordinateBlock);
            coordinateBlock.y = 0.5f;
            Instantiate(Box, coordinateBlock, Quaternion.identity);
            coordinateBlock = coordinateBlock / (1+spaceBlock);
            tileMap[Mathf.RoundToInt(coordinateBlock.x), Mathf.RoundToInt(coordinateBlock.z)] = -1;
            Debug.Log("BlockInstance= "+Mathf.RoundToInt(coordinateBlock.x)+" , "+ Mathf.RoundToInt(coordinateBlock.z));
        }
        foreach(Vector3 removedCoordinate in pointsEnemySpavn)
        {
            emptyCells.Add(removedCoordinate);
        }
        foreach(Vector3 removedCoordinate in wayForPlayer)
        {
            emptyCells.Add(removedCoordinate);
        }
        Debug.Log("player created on = " + playerpos);
    }

    public static bool IsNeibhourCell(Vector3 myCoordinate, Vector3 resultVector)
    {
        return (resultVector == myCoordinate + new Vector3(1 + spaceBlock, 0, 0)) | (resultVector == myCoordinate - new Vector3(1 + spaceBlock, 0, 0))
               | (resultVector == myCoordinate + new Vector3(0, 0, 1 + spaceBlock)) | (resultVector == myCoordinate - new Vector3(0, 0, 1 + spaceBlock));
    }
    void CreateWayForPlayer(Vector3 coordinatePlayed)
    {
        List<Vector3> playerneiqhbour = new List<Vector3>();
        List<Vector3> wayForPlayer = new List<Vector3>();
        int playerPosition = Random.Range(0, emptyCells.Count - 1);
        Vector3 playerpos = emptyCells[playerPosition];
        wayForPlayer.Add(playerpos);
        Instantiate(Playerobj, playerpos, Quaternion.identity);
        emptyCells.RemoveAt(playerPosition);
        Vector3 XY_neiqhbour = playerpos;
        for (int i = 0; i < emptyCellForPlayer; i++)
        {
            foreach (Vector3 vec in emptyCells)
            {
                if (IsNeibhourCell(XY_neiqhbour, vec))
                {
                    playerneiqhbour.Add(vec);
                }
            }
            XY_neiqhbour = playerneiqhbour[Random.Range(0, playerneiqhbour.Count)];
            wayForPlayer.Add(XY_neiqhbour);
            emptyCells.Remove(XY_neiqhbour);
            playerneiqhbour.Clear();
        }
    }
    void CreateFlags()
    {

    }
}