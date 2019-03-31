using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public int chanceCreateBonus;
    public GameObject bonus, bonus2, bonus3, bonus4;
    public void OnTriggerEnter(Collider destroyedObject)
    {
        Vector3 pos = destroyedObject.gameObject.GetComponent<Transform>().position;
        Debug.Log("Explosion destoy box = " + pos);
        int MyCoordinateX = Mathf.RoundToInt(transform.position.x / (1 + InitiatedZone.spaceBlock));
        int MyCoordinateY = Mathf.RoundToInt(transform.position.z / (1 + InitiatedZone.spaceBlock));
        InitiatedZone.tileMap[MyCoordinateX,MyCoordinateY]=0;
        pos.y = 0f;
        InitiatedZone.emptyCells.Add(pos);
        if (Random.Range(0, 100) <= chanceCreateBonus)
        {
            int randBomb = Random.Range(0, 100);
            if (randBomb <= 25)
            {
                Instantiate(bonus, this.transform.position, Quaternion.identity);
            }
            else if(randBomb <=50)
            {
                Instantiate(bonus2, this.transform.position, Quaternion.identity);
            }
            else if(randBomb<= 75)
            {
                Instantiate(bonus3, this.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bonus4, this.transform.position, Quaternion.identity);
            }
        }
        Destroy(this.gameObject);
    }
}
