using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public Vector3 myCoordinate;
    public GameObject trigger;
    public float explosionDistance;
    public int explosionDelay;
    void Start()
    {
        BuildCoordinateToTrigger go = new BuildCoordinateToTrigger();
        myCoordinate = this.gameObject.GetComponent<Transform>().position;
        List<Vector3> result = new List<Vector3>(); 
        result = go.Main(myCoordinate);
        foreach(Vector3 res in result)
        {
            Debug.Log(res);
        }
        //Exploooosion(trigger, myCoordinate, explosionDistance);
        //StartCoroutine(Example());
    }
    IEnumerator Example()
    {
        yield return new WaitForSeconds(explosionDelay);
        //Exploooosion(trigger, myCoordinate, explosionDistance);
        //PlayerController.BombsList.Remove(myCoordinate);
        Destroy(this.gameObject);
    }
}


public class BuildCoordinateToTrigger : BombController
{
public List<Vector3> Main(Vector3 myCoordinate)
    {
        List<Vector3> distanceForTrigger = new List<Vector3>();
        distanceForTrigger = DistanceToWalls(myCoordinate);
        return distanceForTrigger;
    }
    List<Vector3> DistanceToWalls(Vector3 myCoordinate)
    {
        RaycastHit hit;
        List<Vector3> hitList = new List<Vector3>();
        foreach (Vector3 pos in Neighbors())
        {
            Ray ray = new Ray(myCoordinate, pos);
            if (Physics.Raycast(myCoordinate, pos, out hit))
            {
                Vector3 coordinateHit = hit.transform.position;
                coordinateHit -= myCoordinate;
                //coordinateHit.x = Mathf.Round(coordinateHit.x);
                //coordinateHit.z = Mathf.Round(coordinateHit.z);
                coordinateHit.y = 0;
                hitList.Add(coordinateHit);
                Debug.Log("Hit= " + coordinateHit);
            }
        }
        Debug.Log("IsCollisionWalls has been sucsessful");
        return hitList;
    }
    List<Vector3> LimiteToRay(List<Vector3> hitList, float limit)
    {
        List<Vector3> limitedVectors = new List<Vector3>();
        foreach (Vector3 vector in hitList)
        {
            CoordinateUpOrDown(vector, limit);
            if (vector.x > limit)
            {
                limitedVectors.Add(new Vector3(limit, 0, vector.z));
            }
            else if (vector.z > limit)
            {
                limitedVectors.Add(new Vector3(vector.x, 0, limit));
            }
            else { limitedVectors.Add(new Vector3(vector.x, 0, vector.z)); }
        }
        return limitedVectors;
    }

    float CoordinateUpOrDown(Vector3 vector, float limit)
    {
        if (vector.x < 0 | vector.z < 0)
        {
            limit = -limit;
        }
        return limit;
    }
    List<Vector3> Neighbors()
    {
        const int startDist = 1;
        return new List<Vector3>() {new Vector3(startDist, 0, 0),
            new Vector3(-startDist, 0, 0), new Vector3(0, 0, startDist),
            new Vector3(0, 0, -startDist)};
    }
}




class Vremenno : BombController
{
    private List<Vector3> AddLostWector(List<Vector3> distanceToWall, Vector3 myCoordinate, float distance)
    {
        List<bool> coordinateCheck = new List<bool>(4);
        foreach (Vector3 vector in distanceToWall)
        {
            if (vector.x < 0)
            {
                coordinateCheck[0] = true;
                continue;
            }
            else if (vector.x > 0)
            {
                coordinateCheck[1] = true;
                continue;
            }
            else if (vector.z < 0)
            {
                coordinateCheck[2] = true;
                continue;
            }
            else if (vector.z > 0)
            {
                coordinateCheck[3] = true;
                continue;
            }
        }
        int index = 0;
        foreach (bool fantasyLost in coordinateCheck)
        {
            if (fantasyLost == false)
            {
                switch (index = coordinateCheck.IndexOf(fantasyLost))
                {
                    case 0: distanceToWall.Add(new Vector3(-distance, 0, 0)); break;
                    case 1: distanceToWall.Add(new Vector3(distance, 0, 0)); break;
                    case 2: distanceToWall.Add(new Vector3(0, 0, -distance)); break;
                    case 3: distanceToWall.Add(new Vector3(0, 0, distance)); break;
                }
            }
        }
        Debug.Log("AddLostWector has been sucsessful");
        return distanceToWall;
    }

    private bool FindFalse(bool obj)
    {
        return obj == false;
    }


    void Exploooosion(GameObject trigger, Vector3 myCoordinate, float distance)
    {
        const float delayDelete = 5;
        //Build2(IsCollisionWalls(myCoordinate,explosionDistance,transformPos),explosionDistance);
        //BuildTrigger(trigger, myCoordinate, distance, delayDelete, transformPos);
        List<Vector3> result = new List<Vector3>();
////////////////////////////////enter result here////////////////////
        foreach (Vector3 res in result)
        {
            Debug.Log(res);
        }
    }



    void Build2(List<Vector3> distanceToWall, float distance)
    {
        foreach(Vector3 res in distanceToWall)
        {
            //Vector3 buildTrig = myCoordinate + res/2;
            //GameObject explRad = Instantiate(trigger, buildTrig, Quaternion.identity);

        }
    }

    private static void BuildTrigger(GameObject trigger, Vector3 myCoordinate, float distance, float delayDelete, List<Vector3> transformPos)
    {
        int[,] masPositionTriger = new int[transformPos.Count, Mathf.RoundToInt(distance)];
        for (int countCellExpl = 0; countCellExpl <= distance; countCellExpl++)
        {

        }
        for (int countCellExpl = 0; countCellExpl <= distance; countCellExpl++)
        {
            foreach (Vector3 pos in transformPos)
            {
                GameObject explRad = Instantiate(trigger, myCoordinate + (pos * countCellExpl), Quaternion.identity);
                Destroy(explRad, delayDelete);
            }
        }
    }
}

