using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattlerHand : MonoBehaviour
{
    List<Card> list = new List<Card>();

    //手札に追加＝自分の子要素にする
    public void Add(Card card)
    {
        list.Add(card);
        card.transform.parent = transform;
    }

    public void RemoveAllCard()
    {
        list.Clear();
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ResetPosition()
    {
        for (int i = 0; i < list.Count; i++)
        {
            float posX = (i - list.Count/2f) * 1f;
            list[i].transform.localPosition = new Vector3(posX, 0);
        }
    }
    public void FlipCard(int cardNum)
    {
        StartCoroutine(list[cardNum].OpenAnim());
    }
    public void SelectingCard(int cardNum)
    {
        list[cardNum].SelectCard();
    }
    public void UnSelectingCard(int cardNum)
    {
        list[cardNum].UnSelectCard();
    }
}
