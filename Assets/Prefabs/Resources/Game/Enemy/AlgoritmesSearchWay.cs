using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AlgoritmesSearchWay : MonoBehaviour
{
    int index = 0;
    List<Vector3> allNode = new List<Vector3>();
    List<Vector3> currentNodes = new List<Vector3>();
    public GameObject err;
    bool isCoroutine = false;
    //public int distance;
    Vector3 myPos = new Vector3(), oldPos= new Vector3();
    Vector3 targetPosition;
    bool noTarget= true;
    List<Vector3> myWay = new List<Vector3>();
    private void Start()
    {
        
    }
    private void Update()
    {
        if (isCoroutine == false & index==0)
        {
            StartCoroutine(Walk());
        }
    }
    List<Vector3> Search()
    {
        index++;
        myPos = transform.position;
        oldPos = transform.position;
        targetPosition = CoroutineController.playerPosition;
        allNode = InitiatedZone.emptyCells;
        while (noTarget)
        //for (int i=0; i<10; i++)
        {
            SearchNeibhourEmptyCell();
            if (currentNodes.Count == 0)
            {
                allNode.Remove(myPos);
                myPos = oldPos;
            }
            else
            {
                if (currentNodes.Contains(targetPosition))
                {
                    Debug.Log("Дошли ");
                    noTarget = false;
                    myWay.Add(targetPosition);
                    //break;
                }
                else
                {
                    oldPos = myPos;
                    Debug.Log("Target= " + targetPosition);
                    if (currentNodes.Count > 1)
                    {
                        Vector3 MinNode = currentNodes[0];
                        Debug.Log("MinDistanceAfter= " + MinNode + "withDistance= " + ManhattanDistance(MinNode, targetPosition));
                        foreach (Vector3 node in currentNodes)
                        {
                            if (EvristicManhattanFunction(transform.position, node, targetPosition) <= EvristicManhattanFunction(transform.position, MinNode, targetPosition))
                            {
                                MinNode = node;
                            }
                        }
                        currentNodes.Clear();
                        Debug.Log("MinDistanceBefore= " + MinNode + "withDistance= " + ManhattanDistance(MinNode, targetPosition));
                        myWay.Add(MinNode);
                        myPos = MinNode;
                        allNode.Remove(myPos);
                        Instantiate(err, myPos, Quaternion.identity);
                    }
                    else
                    {
                        if (EvristicManhattanFunction(transform.position, currentNodes[0], targetPosition)
                            <EvristicManhattanFunction(transform.position, oldPos, targetPosition))
                        {
                            myWay.Add(currentNodes[0]);
                            Instantiate(err, currentNodes[0], Quaternion.identity);
                        }
                        else
                        {
                            allNode.Remove(currentNodes[0]);
                            myPos = oldPos;
                        }
                    }

                }
            }
        }
        return myWay;
    }

    private void SearchNeibhourEmptyCell()
    {
        foreach (Vector3 node in allNode)
        {
            if (InitiatedZone.IsNeibhourCell(myPos, node))
            {
                currentNodes.Add(node);
                Debug.Log(node);
            }
        }
    }

    IEnumerator Walk()
    {
        isCoroutine = true;
        //noTarget = true;
        List<Vector3> way = Search();
        foreach (Vector3 node in way)
        {
            Vector3 oldPosition = transform.position;
            while ((node - transform.position).magnitude > 0.1)
            {
                yield return new WaitForEndOfFrame();
                transform.position += (node-oldPosition) *Time.deltaTime;
            }
            transform.position = node;
        }
        myWay.Clear();
        isCoroutine = false;
    }


    float ManhattanDistance(Vector3 myCoordinate, Vector3 targetCoordinate)
    {
        return Math.Abs(targetCoordinate.x - myCoordinate.x) + Math.Abs(targetCoordinate.z - myCoordinate.z);
    }
    float EvristicManhattanFunction(Vector3 start, Vector3 point, Vector3 end)
    {
        return ManhattanDistance(point, start) + ManhattanDistance(end, point);
    }
}