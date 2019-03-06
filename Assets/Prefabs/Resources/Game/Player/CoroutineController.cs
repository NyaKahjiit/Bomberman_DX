using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineController : MonoBehaviour
{
    public float maxSpeed;
    bool isCoroutine = false;
    float speedX, speedY;
////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if (isCoroutine == false)
        {
            StartCoroutine(Example());
        }
    }
    ////////////////////////////////////////////////////////
    IEnumerator Example()
    {
        const float deltaSpeedConst = 0.1f;
        float delta;
        isCoroutine = true;
        if (Input.GetAxis("Horizontal") != 0 | Input.GetAxis("Vertical") != 0)
        {
            speedX = 0;
            while (Input.GetAxis("Horizontal") != 0 | Mathf.Abs(speedX) < maxSpeed)
            {
                if (Input.GetAxis("Horizontal") < 0) { delta = -deltaSpeedConst; } else { delta = deltaSpeedConst; }
                SpeedUp(speedX, delta);
                transform.position += BuildVectorWalk(speedX, 1);
                yield return new WaitForEndOfFrame();
            }
            speedY = 0;
            while (Input.GetAxis("Vertical") != 0 | Mathf.Abs(speedY) < maxSpeed)
            {
                if (Input.GetAxis("Vertical") < 0) { delta = -deltaSpeedConst; } else { delta = deltaSpeedConst; }
                SpeedUp(speedY, delta);
                transform.position += BuildVectorWalk(speedY, 2);
                yield return new WaitForEndOfFrame();
            }

        }
        isCoroutine = false;
    }
////////////////////////////////////////////////////
        Vector3 BuildVectorWalk(float speed, int xOrY)
        {
            if (xOrY == 1)
            {
                return new Vector3(Input.GetAxis("Horizontal") + speedX, 0, 0);
            }
            else
            {
                return new Vector3(0, 0, Input.GetAxis("Vertical") + speedY);
            }
        }
////////////////////////////////////////////////////
    void SpeedUp(float speed, float deltaSpeed)
    {
        speed += deltaSpeed;
    }
}
