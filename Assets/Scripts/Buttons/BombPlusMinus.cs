using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BombPlusMinus : MonoBehaviour
{
    [SerializeField] GameSEManager gameSEManager; 
    [SerializeField] Button plusButton;
    [SerializeField] Button minusButton;
    [SerializeField] TMP_Text myBombNumText;
    [SerializeField] GameObject declareButton;
    public int bombNum { get; private set; }
    private void Start() 
    {
        minusButton.interactable = false;
        bombNum = 0;
        myBombNumText.text = bombNum.ToString();
    }

    public void OnPlusButton()
    {
        bombNum++;
        gameSEManager.PMButtonSE();
        myBombNumText.text = bombNum.ToString();
        if (bombNum == 1) plusButton.interactable = false;
        if (bombNum > 0) minusButton.interactable = true;
        declareButton.SetActive(true);
    }
    public void OnMinusButton()
    {
        bombNum--;
        gameSEManager.PMButtonSE();
        myBombNumText.text = bombNum.ToString();
        if (bombNum < 1) plusButton.interactable = true;
        if (bombNum == 0) minusButton.interactable = false;
        declareButton.SetActive(true);
    }
}
