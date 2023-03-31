using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Avatar : MonoBehaviourPunCallbacks
{
    private void Start() 
    {
        SetToParent();
    }
    
    void SetToParent()
    {
        GameObject parent = GameObject.FindWithTag($"player{photonView.ControllerActorNr-1}");
        transform.SetParent(parent.transform, false);      
    }
}
