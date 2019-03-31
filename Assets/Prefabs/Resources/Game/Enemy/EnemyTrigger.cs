using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider otherCollaider)
    {
        if (otherCollaider.tag == "Bomb")
        {
            Debug.Log("You kill Enemy");
            Destroy(this.gameObject);
        }
    }
}
