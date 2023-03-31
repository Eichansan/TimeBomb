using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<int> list = new List<int>();

    private void Start() 
    {
        ListTest();    
    }
    void ListTest()
    {
       for (int i = 0; i < 10; i++)
       {
        list.Add(i);
        Debug.Log("i: "+i+" "+list[i]);
       }
       list[5] = 8;
       for (int i = 0; i < 10; i++)
       {
        Debug.Log("i: "+i+" "+list[i]);
       }
    }

    
}
