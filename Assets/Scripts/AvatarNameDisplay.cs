using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class AvatarNameDisplay : MonoBehaviourPunCallbacks
{
    TMP_Text nameLabel;
    List<string> roles = new List<string>();
    private void Start() 
    {
        nameLabel = GetComponent<TMP_Text>();
        nameLabel.text = $"{photonView.Owner.NickName}";
    }
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        string myRole;
        for (int i = 0; i < 8; i++)
        {
            roles.Add((propertiesThatChanged["R" + i] is string value) ? value : null);
        }
        myRole = roles[PhotonNetwork.LocalPlayer.ActorNumber - 1];
        ShowName(myRole);
    }
    void ShowName(string myRole)
    {
        if(photonView.IsMine) 
        {
            if (myRole.Contains("Bomber"))
            {
                nameLabel.color = Color.red;
            }
            else if (myRole.Contains("TimePolice"))
            {
                nameLabel.color = Color.blue;
            }
        }
        nameLabel.text = $"{photonView.Owner.NickName}";
    }
}
