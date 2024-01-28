using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SuccessPlusMinus : MonoBehaviour
{
    [SerializeField] GameSEManager gameSEManager; 
    [SerializeField] Button plusButton;
    [SerializeField] Button minusButton;
    [SerializeField] TMP_Text mySuccessNumText;
    [SerializeField] GameObject declareButton;
    public int successNum { get; private set; }
    private void Start() 
    {
        minusButton.interactable = false;
        successNum = 0; 
        mySuccessNumText.text = successNum.ToString();
    }

    public void OnPlusButton()
    {
        successNum++;
        gameSEManager.PMButtonSE();
        mySuccessNumText.text = successNum.ToString();
        if (successNum == GameDataManager.Instance.players) plusButton.interactable = false;
        if (successNum > 0) minusButton.interactable = true;
        declareButton.SetActive(true);
    }
    public void OnMinusButton()
    {
        successNum--;
        gameSEManager.PMButtonSE();
        mySuccessNumText.text = successNum.ToString();
        if (successNum < GameDataManager.Instance.players) plusButton.interactable = true;
        if (successNum == 0) minusButton.interactable = false;
        declareButton.SetActive(true);
    }
}
