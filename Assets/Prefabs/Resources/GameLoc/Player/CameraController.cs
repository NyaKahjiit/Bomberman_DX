using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseWhellSpeed;
    private Vector3 position, UpdPosition;
    private Quaternion rotation;
    private GameObject cameraObj;
    private float distance = 0;
    private Vector3 defaultTranslation = new Vector3(0, 5, -1);
    private const float maxZoom = 0f, minZoom= 10f, stepIfNotLimit= 0.1f;
    void Start()
    {
        cameraObj = GameObject.Find("Main Camera");
        position = this.GetComponent<Transform>().position;
        UpdPosition = position;
        rotation = Quaternion.Euler(60, 0, 0);
        cameraObj.GetComponent<Transform>().SetPositionAndRotation(position+defaultTranslation, rotation);
    }
    void Update()
    {
        position = this.GetComponent<Transform>().position + defaultTranslation;
        if (distance<=maxZoom) 
        {
            distance += stepIfNotLimit;
        }
        if (distance >= minZoom)
        {
            distance -= stepIfNotLimit;
        }
        else
        {
            distance += (Input.GetAxis("Mouse ScrollWheel"))*mouseWhellSpeed;
        }
        UpdPosition.y = position.y + distance;
        UpdPosition.z = position.z - distance;
        UpdPosition.x = position.x;
        cameraObj.transform.position = UpdPosition;

    }
}
