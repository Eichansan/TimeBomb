using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] GameObject lampOnObj;
    [SerializeField] GameObject lampOffObj;

    public void Init()
    {
        lampOnObj.SetActive(false);
        lampOffObj.SetActive(true);
    }
    public void On()
    {
        lampOnObj.SetActive(true);
        lampOffObj.SetActive(false);
    }
}
