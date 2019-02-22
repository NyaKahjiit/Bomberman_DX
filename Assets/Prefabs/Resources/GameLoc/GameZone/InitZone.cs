using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitZone : MonoBehaviour
{
    public void Start()
    {
        LObject.LZone go = new LObject.LZone();
        int X_count = PlayerPrefs.GetInt("X_count");
        int Y_count = PlayerPrefs.GetInt("Y_count");
        GameObject Field = go.LField();
        GameObject WallBlock = go.LWall();
        float zx = 0, zy = 0, x_field=0, y_field=0;
        for (int i = 0; i < X_count ; i++)
        {
            zy = 0;
            for (int j = 0; j < Y_count; j++)
            {
                if (i == 0 | j == 0 | i == X_count - 1 | j == Y_count - 1 | (i%2==0 & j%2==0))
                {
                    Instantiate(WallBlock, new Vector3(i+zx, 0.5f, j+zy), Quaternion.identity);
                }
                if (i == X_count - 1 & j == Y_count - 1)
                {
                    x_field = (i + zx);
                    y_field = (j + zy);
                }
                zy += 0.2f;
            }
            zx += 0.2f;
        }
        GameObject newObject = Instantiate(Field, new Vector3((x_field)/2 , 0, (y_field)/2), Quaternion.identity) as GameObject;
        newObject.transform.localScale = new Vector3((x_field+1)/10 , 1, (y_field+1) / 10);
        Debug.Log(X_count);
    }

}
