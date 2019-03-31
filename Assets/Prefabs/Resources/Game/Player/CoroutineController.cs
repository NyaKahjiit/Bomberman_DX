using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutineController : MonoBehaviour
{
    public static float speed;
    public static bool isCoroutine = false, moveOnBox=false;
    private Vector3 position;
    private float spaceCell;
    public static Vector3 playerPosition;
    public AudioSource step;
    private void Start()
    {
        //moveVector = new Vector3();
        speed = 2f;
        playerPosition = transform.position;
        position = transform.position;
        spaceCell = InitiatedZone.spaceBlock;
    }
    void Update()
    {
        if (isCoroutine == false & TriggerDamage.isGameOver==false & (Input.GetAxis("Horizontal") != 0 | Input.GetAxis("Vertical") != 0))
        {
            StartCoroutine(Walk(speed, position));
        }
    }
    IEnumerator Walk(float speed, Vector3 myPosition)
    {
        step.Play();
        Vector3 deltaPosition = new Vector3();
        isCoroutine = true;
        GetComponent<Animator>().SetBool("walk", true);
        deltaPosition = InputControl(deltaPosition);
        if (Uslmove(myPosition,deltaPosition))
        {
            myPosition += deltaPosition;
            SetRotate(deltaPosition);
            while ((myPosition - transform.position).magnitude > 0.2)
            {
                transform.position += (deltaPosition/60)*speed;
                yield return new WaitForEndOfFrame();
            }
            transform.position = myPosition;
            position = myPosition;
        }
        playerPosition = myPosition;
        isCoroutine = false;
        GetComponent<Animator>().SetBool("walk", false);
        step.Stop();
    }

    bool Uslmove(Vector3 myPosition, Vector3 deltaPosition)
    {
        int position = InitiatedZone.tileMap[Mathf.RoundToInt((myPosition + deltaPosition).x / (1 + spaceCell)), Mathf.RoundToInt((myPosition + deltaPosition).z / (1 + spaceCell))];
        if (moveOnBox == false)
        {
            return position == 0;
        }
        else
        {
            return position != -5;
        }
    }

    Vector3 InputControl(Vector3 deltaPosition)
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            return deltaPosition = new Vector3(0, 0, (1 + spaceCell));
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            return deltaPosition = new Vector3(0, 0, -(1 + spaceCell));
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            return deltaPosition = new Vector3((1 + spaceCell), 0, 0);
        }
        else
        {
            return deltaPosition = new Vector3(-(1 + spaceCell), 0, 0);
        }
    }

    void SetRotate(Vector3 deltaPosition)
    {
        if (deltaPosition.x > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        if (deltaPosition.x < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
        }
        if (deltaPosition.z > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        if (deltaPosition.z < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }
}
