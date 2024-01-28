using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class CardGenerator : MonoBehaviourPunCallbacks
{
    [SerializeField] Card cardPrefab;
    [SerializeField] List<CardBase> CBlist = new List<CardBase>();
    public List<int> typeNumList = new List<int>();
    bool isOther;
    
    public void BuildCardList()
    {
        int success=0,boom=0;
        for (int i = 0; i < 5*GameDataManager.Instance.players; i++)
        {   
            if (boom < 1)
            {
                typeNumList.Add(UnityEngine.Random.Range(0,3));
                if (typeNumList[typeNumList.Count-1]==2)boom++;
                else if (typeNumList[typeNumList.Count-1]==1)success++;
            }
            else if (success < GameDataManager.Instance.players)
            {
                typeNumList.Add(UnityEngine.Random.Range(0,2));
                if (typeNumList[typeNumList.Count-1]==1)success++;
            }
            else 
            {
                typeNumList.Add(0);
            }
        }
    }
    public void CardShuffle()//
    {
        typeNumList = typeNumList.OrderBy(a => Guid.NewGuid()).ToList();
    }
    public void SendCardListData()//typeNumListを送る
    {
        var hashtable = new ExitGames.Client.Photon.Hashtable();
        for (int i = 0; i < typeNumList.Count; i++)
        {
            hashtable["No" + i] = typeNumList[i];
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
        hashtable.Clear();
    }
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)//
    {
        for (int i = 0; i < typeNumList.Count; i++)
        {
            typeNumList[i] = (propertiesThatChanged["No" + i] is int value) ? value : 0;
        }
    }
    public Card Spawn(int c_i,int actorNum,int playerNum)
    {
        if (actorNum==playerNum) 
        {
            isOther = false;
        }
        else 
        {
            isOther = true;
        }
        Card card = Instantiate(cardPrefab);
        card.Set(c_i, CBlist[typeNumList[c_i]], isOther);
        return card;
    }

    public void RemoveBases(int silent,int success,int Boom)
    {
        if (silent > 0)
        {
            for (int i = 0; i < silent; i++)
            {
                typeNumList.Remove(0);
            }
        }
        if (success > 0)
        {
            for (int i = 0; i < success; i++)
            {
                typeNumList.Remove(1);
            }
        }
        if (Boom > 0)typeNumList.Remove(2);//別に要らない
    }
}
