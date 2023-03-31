using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{
    [SerializeField] Font silentFont;
    [SerializeField] Font boomFont;
    [SerializeField] Image gameResultImage;
    [SerializeField] Text roleText;
    [SerializeField] Text roundText;  
    [SerializeField] Text yourTurnText; 
    [SerializeField] Text turnResultText; 
    [SerializeField] Text gameResultText;
    [SerializeField] GameObject gameResultPanel;
    [SerializeField] GameObject leavePanel;
    [SerializeField] GameObject retryMessage;
    public void Init()
    {
        turnResultText.font = silentFont;
        roleText.gameObject.SetActive(false);
        roundText.gameObject.SetActive(false);
        yourTurnText.gameObject.SetActive(false);
        turnResultText.gameObject.SetActive(false);
        gameResultPanel.SetActive(false);
        leavePanel.SetActive(false);
        retryMessage.SetActive(false);
    }
    public void ShowRole(string role)
    {
        if (role == "TimePolice") roleText.color = Color.blue;
        else if (role == "Bomber") roleText.color = Color.red;
        roleText.gameObject.SetActive(true);
        roleText.text =(role);
    }
    public void ShowRound(int round)
    {
        roundText.gameObject.SetActive(true);
        roundText.text ="Round" + " " + round.ToString();
    }
    public void ShowYourTurn(string role)
    {
        if (role == "TimePolice") yourTurnText.color = Color.blue;
        else if (role == "Bomber") yourTurnText.color = Color.red;
        StartCoroutine(FadeOutYourTurn(yourTurnText.gameObject, 2));
    }
    public void ShowTurnResult(string result)
    {
        turnResultText.gameObject.SetActive(true);
        if (result == "Boom!")
        {
            turnResultText.font = boomFont;
            turnResultText.fontSize = 300;
        }
        turnResultText.text = result;
    }
    public void ShowGameResult(string winner)
    {
        gameResultPanel.SetActive(true);
        if (winner == "POLICE") gameResultImage.color = new Color32 (0, 70, 171, 255);
        else if (winner == "BOMBER") gameResultImage.color = new Color32 (210, 30, 0, 255);
        gameResultText.text = winner + " " + "WIN";
    }
    public void ShowLeavePanel()
    {
        leavePanel.SetActive(true);
    }
    public void ShowRetryMessage(int retryHopeNum)
    {
        retryMessage.SetActive(true);
        Text retryMess = retryMessage.GetComponent<Text>();
        retryMess.text = $"{retryHopeNum}クルーが再戦を希望";
    }
    public void SetupNextTurn()
    {
        turnResultText.gameObject.SetActive(false);
    }
    public void SetupNextRound()
    {
        roundText.gameObject.SetActive(false);
    }
    public void HideRoleDisPlay()
    {
        roleText.gameObject.SetActive(false);
    }
    IEnumerator FadeOutYourTurn(GameObject gameObject,int times)
    {
        gameObject.SetActive(true);
        float speed = 0.01f;
        for (int i=0; i < times; i++)
        {
            while (yourTurnText.color.a < 1)
            {
                yourTurnText.color = new Color(yourTurnText.color.r, yourTurnText.color.g, yourTurnText.color.b, yourTurnText.color.a + speed);
                yield return new WaitForSeconds(speed);
            }
            while (yourTurnText.color.a > 0)
            {
                yourTurnText.color = new Color(yourTurnText.color.r, yourTurnText.color.g, yourTurnText.color.b, yourTurnText.color.a - speed);
                yield return new WaitForSeconds(speed);
            }
        }
        gameObject.SetActive(false);
    }
}
