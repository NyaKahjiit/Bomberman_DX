using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public Vector3 myCoordinate;
    public GameObject trigger;
    public int explosionDistance;
    public int explosionDelay;
    void Start()
    {
        BuilderCoordinateToTrigger go = new BuilderCoordinateToTrigger();
        myCoordinate = this.gameObject.GetComponent<Transform>().position;
        List<Vector3> result = new List<Vector3>(); 
        result = go.BuildCoordinate(myCoordinate, explosionDistance);
        foreach(Vector3 res in result)
        {
            Debug.Log(res);
        }
        foreach(Vector3 buildCoordinate in result)
        {
            Instantiate(trigger, (myCoordinate + buildCoordinate) * 0.5f, Quaternion.identity);
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


public class BuilderCoordinateToTrigger : BombController
{
    List<Vector3> hitList = new List<Vector3>();
    public List<Vector3> BuildCoordinate(Vector3 myCoordinate, int distance)
    {
        List<Vector3> distanceForTrigger = new List<Vector3>();
        DistanceToWalls(myCoordinate);
        LimiteToRay(distance);
        foreach(Vector3 vec in hitList)
        {
            Debug.Log(vec);
        }
        return distanceForTrigger;
    }
    void DistanceToWalls(Vector3 myCoordinate)
    {
        RaycastHit hit;
        foreach (Vector3 pos in Neighbors())
        {
            Ray ray = new Ray(myCoordinate, pos);
            if (Physics.Raycast(myCoordinate, pos, out hit))
            {
                Vector3 coordinateHit = hit.transform.position;
                coordinateHit -= myCoordinate;
                coordinateHit.x = Mathf.Round(coordinateHit.x);
                coordinateHit.z = Mathf.Round(coordinateHit.z);
                coordinateHit.y = 0;
                hitList.Add(coordinateHit);
            }
        }
        Debug.Log("IsCollisionWalls has been sucsessful");
    }
    void LimiteToRay(int limit)
    {
        int localLimit;
        for (int i=0; i<hitList.Count; i++)
        {
            localLimit = limit;
            if (Mathf.Abs(hitList[i].x)>localLimit)
            {
                if (hitList[i].x < 0) { localLimit = -localLimit; }
                hitList[i] = new Vector3 (localLimit,0,0);
            }
            else if (Mathf.Abs(hitList[i].z) > localLimit)
            {
                if (hitList[i].z < 0) { localLimit = -localLimit; }
                hitList[i] = new Vector3(0, 0, localLimit);
            }
        }
        Debug.Log("LimiteToRay has been sucsessful");
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
    void Exploooosion(GameObject trigger, Vector3 myCoordinate, float distance)
    {
        //Build2(IsCollisionWalls(myCoordinate,explosionDistance,transformPos),explosionDistance);
        //BuildTrigger(trigger, myCoordinate, distance, delayDelete, transformPos);
        List<Vector3> result = new List<Vector3>();
////////////////////////////////enter result here////////////////////
        foreach (Vector3 res in result)
        {
            Debug.Log(res);
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

