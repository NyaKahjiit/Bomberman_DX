using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider destroyedObject)
    {
        Vector3 pos = destroyedObject.gameObject.GetComponent<Transform>().position;
        Debug.Log("Explosion destoy box = " + pos);
        Destroy(this.gameObject);
    }
}
