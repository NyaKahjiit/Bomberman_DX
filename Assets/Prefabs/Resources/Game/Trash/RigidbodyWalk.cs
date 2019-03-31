using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyWalk : MonoBehaviour
{
    public int speed;
    private Vector3 playvec;
    void Update()
    {
        playvec = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        transform.LookAt(playvec + transform.position);
        this.GetComponent<Rigidbody>().velocity = playvec;
    }
}