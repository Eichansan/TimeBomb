using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using DG.Tweening;
public class Card : MonoBehaviourPunCallbacks//, IPunObservable
{
    // カードUI
    //　ゲーム内の処理
    [SerializeField] Image cardImage;
    public int cardNum { get; private set;}
    // [SerializeField] Image icon;
    // [SerializeField] Text descriptionText;
    [SerializeField] public GameObject hideOtherPanel;
    [SerializeField] public GameObject hideMyPanel;
    [SerializeField] public GameObject outlinePanel;
    Image hidePanelImage;
    public CardBase Base { get; private set;} 
    public UnityAction<Card> OnClickCard;//関数を登録できる 
    public void Set(int c_i,CardBase cardBase,bool isOther)
    {
        Base = cardBase;
        cardImage.sprite = cardBase.CardImage;
        cardNum = c_i;
        outlinePanel.SetActive(false);
        if (isOther)
        {
            hideOtherPanel.SetActive(true);
            hideMyPanel.SetActive(false);
        }
        else if (!(isOther))
        {
            hideMyPanel.SetActive(true);
            hideOtherPanel.SetActive(false);
        }
    }
    public IEnumerator OpenAnim()
    {
        yield return transform.DORotate(new Vector3(0, 90, 0), 0.2f).WaitForCompletion();
        hideOtherPanel.SetActive(false);
        hideMyPanel.SetActive(false);
        yield return transform.DORotate(new Vector3(0, 0, 0), 0.2f).WaitForCompletion();
    }
    public void SelectCard()
    {
        outlinePanel.SetActive(true);
        transform.position += Vector3.up * 0.3f;
        transform.position = new Vector3(transform.position.x,transform.position.y,-1);
        transform.localScale = Vector3.one*0.5f * 1.3f * 1.4f;
    }
    public void UnSelectCard()
    {
        outlinePanel.SetActive(false);
        transform.position -= Vector3.up * 0.3f;
        transform.position = new Vector3(transform.position.x,transform.position.y,0);
        transform.localScale = Vector3.one*0.5f;
    }
    public void PointerEnterOther()
    {
        transform.position += Vector3.up * 0.3f;
        transform.localScale *= 1.3f;
        GetComponentInChildren<Canvas>().sortingLayerName ="Overlay";
    }
    public void PointerExitOther()
    {
        transform.position -= Vector3.up * 0.3f;
        transform.localScale /= 1.3f;
        GetComponentInChildren<Canvas>().sortingLayerName ="Default";
    }
    public void PointerEnterMine()
    {
        hidePanelImage = hideMyPanel.GetComponent<Image>();
        hidePanelImage.color = new Color(hidePanelImage.color.r, hidePanelImage.color.g, hidePanelImage.color.b, 0f);
        transform.localScale = Vector3.one*0.5f * 1.3f;
        GetComponentInChildren<Canvas>().sortingLayerName ="Overlay";
    }
    public void PointerExitMine()
    {
        hidePanelImage.color = new Color(hidePanelImage.color.r, hidePanelImage.color.g, hidePanelImage.color.b, 1f);
        transform.localScale = Vector3.one * 0.5f;
        GetComponentInChildren<Canvas>().sortingLayerName ="Default";
    }
    public void OnClick()
    {
        OnClickCard?.Invoke(this);
    }

}
