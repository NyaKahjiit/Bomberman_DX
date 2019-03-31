using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkController : MonoBehaviour
{
    bool isCoroutine = false;
    public float speed;
    List<Vector3> allPossibleMoves = new List<Vector3>();
    Vector3 oldPosition = new Vector3();
    Vector3 startPosition = new Vector3();
    private void Start()
    {
        oldPosition = transform.position;
        startPosition = transform.position;
    }
    private void Update()
    {
        if (isCoroutine == false)
        {
            StartCoroutine(StupidWalk(speed, startPosition));
        }
    }

    IEnumerator StupidWalk(float speed, Vector3 position)
    {
        GetComponent<Animator>().SetBool("isWalk", true);
        isCoroutine = true;
        allPossibleMoves = InitiatedZone.emptyCells;
        List<Vector3> currentPossibleMoves = new List<Vector3>();
        foreach (Vector3 vec in allPossibleMoves)
        {
            if (InitiatedZone.IsNeibhourCell(position, vec))
            {
                currentPossibleMoves.Add(vec);
            }
        }
        if (currentPossibleMoves.Count > 1)
        {
            currentPossibleMoves.Remove(oldPosition);
        }
        oldPosition = position;
        position = currentPossibleMoves[Random.Range(0, currentPossibleMoves.Count)];
        currentPossibleMoves.Clear();
        Vector3 moveVector = (position - oldPosition);
        transform.LookAt(position);
        if (position == CoroutineController.playerPosition)
        {
            GetComponent<Animator>().SetBool("isWalk", false);
            GetComponent<Animator>().SetTrigger("endGame");
        }
        else
        {
        
        while ((position - transform.position).magnitude > 0.1)
        {
            yield return new WaitForEndOfFrame();
            transform.position += moveVector * Time.deltaTime * speed;
        }
        transform.position = position;
        startPosition = position;
        InitiatedZone.playerPosition = position;
        isCoroutine = false;
        GetComponent<Animator>().SetBool("isWalk", false);
        }
    }
}
