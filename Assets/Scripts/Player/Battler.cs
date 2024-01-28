using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Battler : MonoBehaviourPunCallbacks
{
    [SerializeField] GameMaster gm;
    [SerializeField] BattlerHand hand;
    [SerializeField] DecideCard decideCard;
    public BattlerHand Hand { get => hand; }
    public int actorNum { get; private set;}
    public int playerNum { get; private set;}
    public void Init()
    {
      actorNum = GameDataManager.Instance.actorNum;
      playerNum = int.Parse(this.gameObject.ToString()[6].ToString());
    }
    public void SetCardToHand(Card card)
    {
      hand.Add(card);
      card.OnClickCard = SelectedCard;
    }
    public void RemoveCardFromHand()
    {
      hand.RemoveAllCard();
    }
    //カードがなにか調べる
    void SelectedCard(Card card)
    {
      if (decideCard.isDecided)
      {
        return;
      }
      if (gm.nipperPlayerNum == actorNum) decideCard.Set(card, playerNum);
    }
    public void FlipCard(int cardNum)
    {
        hand.FlipCard(cardNum);
    }
    public void SelectingCard(int cardNum)
    {
        hand.SelectingCard(cardNum);
    }
    public void UnSelectingCard(int cardNum)
    {
        hand.UnSelectingCard(cardNum);
    }

}
