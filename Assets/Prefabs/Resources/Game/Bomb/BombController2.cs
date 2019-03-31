using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController2 : MonoBehaviour
{
    public int delay;
    public AudioSource audExplosion;
    List<Vector3> zoneExplosion = new List<Vector3>();
    public GameObject trig;
    public GameObject partSys;
    int limit;
    Vector3 updatePosition = new Vector3();
    private const float delayDeleteTrigger=1.0f;
    void Start()
    {
        limit = CreateBomb.limit;
        StartCoroutine(Explosioooon());
    }
    IEnumerator Explosioooon()
    {
        yield return new WaitForSeconds(delay);
        AudioSource audio = Instantiate(audExplosion, transform.position, Quaternion.identity);
        audio.Play();
        Destroy(audio.gameObject, 2f);
        SearchCoordinateBuildTrigger();
        BuildAndDeleteTrigger();
        CreateBomb.BombsList.Remove(transform.position);
        Vector3 partSystCoord = transform.position + new Vector3(0, 0.3f, 0);
        Instantiate(partSys, partSystCoord, Quaternion.identity);
        Destroy(this.gameObject);
    }
    void SearchCoordinateBuildTrigger()
    {
        int myPositionX = Mathf.RoundToInt(transform.position.x / (1 + InitiatedZone.spaceBlock));
        int myPositionY = Mathf.RoundToInt(transform.position.z / (1 + InitiatedZone.spaceBlock));
        Debug.Log("Me= " + myPositionX + " , " + myPositionY);

        int currentPosition = myPositionX + 1;
        while ((currentPosition != myPositionX + limit + 1) & (InitiatedZone.tileMap[currentPosition, myPositionY] != -5))
        {
            zoneExplosion.Add(new Vector3(currentPosition,0.5f ,myPositionY));
            currentPosition++;
        }
        currentPosition = myPositionX - 1;
        while ((currentPosition != myPositionX - limit - 1) & (InitiatedZone.tileMap[currentPosition, myPositionY] != -5))
        {
            zoneExplosion.Add(new Vector3(currentPosition, 0.5f ,myPositionY));
            currentPosition--;
        }
        currentPosition = myPositionY + 1;
        while ((currentPosition != myPositionY + limit + 1) & (InitiatedZone.tileMap[myPositionX, currentPosition] != -5))
        {
            zoneExplosion.Add(new Vector3(myPositionX,0.5f,currentPosition));
            currentPosition++;
        }
        currentPosition = myPositionY - 1;
        while ((currentPosition != myPositionY - limit - 1) & (InitiatedZone.tileMap[myPositionX, currentPosition] != -5))
        {
            zoneExplosion.Add(new Vector3(myPositionX,0.5f,currentPosition));
            currentPosition--;
        }
    }
    void BuildAndDeleteTrigger()
    {
        foreach (Vector3 trigPosition in zoneExplosion)
        {
            updatePosition = trigPosition;
            updatePosition.x = trigPosition.x + (trigPosition.x * InitiatedZone.spaceBlock);
            updatePosition.z = trigPosition.z + (trigPosition.z * InitiatedZone.spaceBlock);
            GameObject trigger = Instantiate(trig, updatePosition, Quaternion.identity);
            Destroy(trigger, delayDeleteTrigger);
        }
    }
}
