using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class DecideCard : MonoBehaviourPunCallbacks
{
    [SerializeField] GameSEManager gameSEManager; 
    [SerializeField] GameObject decideButton;
    [SerializeField] GameMaster gameMaster;
    public Card selectedCard { get; private set;}
    // public Card tmpCard { get; private set;}

    public UnityAction OnDecideAction;
    public bool isDecided { get; set;}
    public int selectedPlayerNum { get; private set;}
    public int decidedCardTypeNum { get; private set;}
    public int decidedCardNum { get; private set;}
    public int tmp_listNum { get; private set;} = -1;
    public int tmp_pNum { get; private set;}
    public void Set(Card card, int playerNum)
    {
        gameSEManager.CardSelectSE();
        decideButton.SetActive(true);
        selectedCard?.UnSelectCard();
        selectedCard = card;
        selectedPlayerNum = playerNum;
        selectedCard.SelectCard();
        int selectingCardNum = selectedCard.cardNum;
        photonView.RPC(nameof(Set_Others), RpcTarget.Others, selectingCardNum);
    }
    [PunRPC]
    void Set_Others(int selectingCardNum)
    {
        gameSEManager.CardSelectSE();
        if (tmp_listNum >= 0)gameMaster.player[tmp_pNum].UnSelectingCard(tmp_listNum);
        int round = gameMaster.round;
        tmp_pNum = selectingCardNum/(6-round);
        tmp_listNum = selectingCardNum%(6-round);
        gameMaster.player[tmp_pNum].SelectingCard(tmp_listNum);
    } 

    public void OnDecideButton()//切断ボタンを押したら実行される//押した本人にしか実行されない
    {   
        isDecided = true;
        decidedCardTypeNum = selectedCard.Base.Number;
        decidedCardNum = selectedCard.cardNum;
        OnDecideAction?.Invoke();
        decideButton.SetActive(false);
    }
    public void SetupNextTurn()
    {
        StartCoroutine(selectedCard.OpenAnim());
        selectedCard.UnSelectCard();
        photonView.RPC(nameof(SetupNextTurn_Others), RpcTarget.Others, decidedCardNum);
        isDecided = false;
        selectedCard = null;
    }
    [PunRPC]
    void SetupNextTurn_Others(int decidedCardNum)
    {
        int round = gameMaster.round;
        tmp_pNum = decidedCardNum/(6-round);
        tmp_listNum = decidedCardNum%(6-round);
        gameMaster.player[tmp_pNum].UnSelectingCard(tmp_listNum);
        tmp_pNum = tmp_listNum = -1;
    } 
}
