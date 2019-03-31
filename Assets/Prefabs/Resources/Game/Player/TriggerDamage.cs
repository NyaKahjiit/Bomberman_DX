using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDamage : MonoBehaviour
{
    public ParticleSystem tail;
    public AudioSource Death;
    public static bool isGameOver=false;
    public GameObject rad;
    public void OnTriggerEnter(Collider otherCollaider)
    {
        if (otherCollaider.tag == "Enemy" | otherCollaider.tag == "Bomb")
        {
            GetComponent<Animator>().SetBool("gameOver", true);
            Death.Play();
            isGameOver = true;
        }
        if (otherCollaider.tag == "Bonus")
        {
            CreateBomb.limit += 1;
            StartCoroutine(ShowRadius());
            Destroy(otherCollaider.gameObject);
        }
        if (otherCollaider.tag == "Bonus2")
        {
            CoroutineController.speed = 5;
            Destroy(otherCollaider.gameObject);
            tail.gameObject.SetActive(true);
        }
        if (otherCollaider.tag == "Bonus3")
        {
            GetComponent<Animator>().SetTrigger("pickBonus3");
            CreateBomb.limBombs+=1;
            Destroy(otherCollaider.gameObject);
        }
        if (otherCollaider.tag == "Bonus4")
        {
            CoroutineController.moveOnBox = true;
            Destroy(otherCollaider.gameObject);
        }
    }
    void OnGUI()
    {
        var pauseskin = GUI.skin.GetStyle("Label");
        pauseskin.fontSize = 20;
        pauseskin.alignment = TextAnchor.UpperCenter;
        if (isGameOver == true)
        {
            //Time.timeScale = 0;
            GUI.Label(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, 150f, 45f), "You Die", pauseskin);
            Cursor.visible = true;
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 30f, 150f, 45f), "Начать заново"))
            {
                SceneManager.LoadScene("Game");
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) + 30f, 150f, 45f), "В Меню"))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
    IEnumerator ShowRadius()
    {
        bool isActive = true;
        List<GameObject> spheres = new List<GameObject>();
        GameObject sphere = new GameObject();
        for (int i=1; i<=CreateBomb.limit; i++)
        {
            List<Vector3> vecs = new List<Vector3>();
            vecs.Add(new Vector3(transform.position.x + i, 1, transform.position.z));
            vecs.Add(new Vector3(transform.position.x - i, 1, transform.position.z));
            vecs.Add(new Vector3(transform.position.x, 1, transform.position.z+ i));
            vecs.Add(new Vector3(transform.position.x, 1, transform.position.z - i));
            foreach (Vector3 vec in vecs)
            {
                sphere = Instantiate(rad, vec, Quaternion.identity);
                spheres.Add(sphere);
            } 
        }
        for(int iterac=0; iterac<6; iterac++)
        {
            foreach (GameObject x in spheres)
            {
                x.SetActive(isActive);
            }
            isActive = !isActive;
            yield return new WaitForSeconds(0.5f);
        }
        foreach(GameObject spher in spheres)
        {
            Destroy(spher);
        }
        
    }
}
