using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParam : MonoBehaviour
{
    public string Text;
    public int X_count=9, Y_count=9;
    public void INsert_X()
    {
        GameObject.Find("scaleField_x");
        string x= this.Text;
        Debug.Log(x);
    }
}
