using System.Security.Cryptography;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoleManager : MonoBehaviourPunCallbacks
{
    int players;
    int firstNipperPlayer;
    public List<string> roles = new List<string>();


    public void BuildRoles()
    {
        players = GameDataManager.Instance.players;
        roles.AddRange(new List<string>() { "Bomber", "Bomber","TimePolice","TimePolice","TimePolice","TimePolice" });
        if(players >= 7)
        {
            roles.AddRange(new List<string>() { "Bomber","TimePolice" });
        }
    }
    public void RoleShuffle()
    {
        firstNipperPlayer = UnityEngine.Random.Range(0,GameDataManager.Instance.players);
        roles = roles.OrderBy(a => Guid.NewGuid()).ToList();
        Debug.Log("finishShaffule");
    }
    public void SendRoleData()
    {
        var hashtable = new ExitGames.Client.Photon.Hashtable();
        hashtable["f"] = firstNipperPlayer;
        for (int i = 0; i < roles.Count; i++)
        {
            hashtable["R" + i] = roles[i];
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
        hashtable.Clear();
        for(int i=0; i < roles.Count; i++)Debug.Log("i"+i+" "+roles[i]);
    }
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        for (int i = 0; i < roles.Count; i++)
        {
            roles[i] = (propertiesThatChanged["R" + i] is string value) ? value : null;
            Debug.Log("i"+i+" "+roles[i]);
        }
        firstNipperPlayer = (propertiesThatChanged["f"] is int f_value) ? f_value : 0;
    }
    public string GetRole(int actorNum)
    {
        string myRole = roles[actorNum];
        return myRole;
    }
    public int GetFirstNipperNum()
    {
        return firstNipperPlayer;
    }
}
