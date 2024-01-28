using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class Declare : MonoBehaviourPunCallbacks
{
    [SerializeField] GameSEManager gameSEManager; 
    [SerializeField] SuccessPlusMinus successPlusMinus; 
    [SerializeField] BombPlusMinus bombPlusMinus; 
    public List<TMP_Text> successText = new List<TMP_Text>();
    public List<TMP_Text> bombText = new List<TMP_Text>();
    public void OnClick()
    {
        gameSEManager.OnDecideButtonSE();
        this.gameObject.SetActive(false);
        photonView.RPC(nameof(SendDeclaredNum), RpcTarget.Others, GameDataManager.Instance.actorNum, successPlusMinus.successNum, bombPlusMinus.bombNum);
    }

    [PunRPC]
    void SendDeclaredNum(int actorNum,int successNum,int bombNum)
    {
        if (actorNum == 0)
        {
            successText[GameDataManager.Instance.actorNum].text = successNum.ToString();
            bombText[GameDataManager.Instance.actorNum].text = bombNum.ToString();
        }
        else 
        {
            successText[actorNum].text = successNum.ToString();
            bombText[actorNum].text = bombNum.ToString();
        }
    }
    public void SetupNextRound()
    {
        for (int i = 0; i < GameDataManager.Instance.players; i++)
        {
            successText[i].text = null;
            bombText[i].text = null;
        }
    }
}
