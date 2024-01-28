using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameMaster : MonoBehaviourPunCallbacks
{
    [SerializeField] GameSEManager gameSEManager; 
    [SerializeField] CardGenerator cardGenarator; 
    [SerializeField] MouseManager mouseManager; 
    [SerializeField] public Battler[] player;
    [SerializeField] List<Lamp> lamp = new List<Lamp>();
    [SerializeField] DecideCard decideCard;
    [SerializeField] GameUI gameUI;
    [SerializeField] Declare declare;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject declareObjs;
    RuleBook ruleBook;
    RoleManager roleManager;
    public int actorNum { get; private set;}
    private int preNipperPlayerNum;
    public int nipperPlayerNum { get; private set;}
    string myRole;
    public int round = 1;
    int players;
    int turnCount;
    int silent,success,Boom;
    int sum_success;
    int retryHopeNum;
    bool isStarting;
    private void Awake() 
    {
        nipperPlayerNum = 0;
        actorNum = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        players = GameDataManager.Instance.players;
        GameDataManager.Instance.actorNum = actorNum;
        for (int i=0; i<players; i++)player[i].Init();
        declareObjs.SetActive(false);
        gameUI.Init();
        SetAvatar();
        SetPlayerPosition();
        ruleBook = GetComponent<RuleBook>();
        roleManager = GetComponent<RoleManager>();
        roleManager.BuildRoles();
        if (PhotonNetwork.IsMasterClient)
        {
            roleManager.RoleShuffle();
        }
    }
    private void Update() 
    {
        if (isStarting)
        {
            return;
        }
        if (GameDataManager.Instance.inRoom)
        {
            if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                isStarting = true;
                if (PhotonNetwork.IsMasterClient)
                {
                    startButton.SetActive(true);
                }
            }
        }
    }

    public void OnStartButton() 
    {
        gameSEManager.OnDecideButtonSE();
        startButton.SetActive(false);
        roleManager.SendRoleData();
        photonView.RPC(nameof(RPCSetCoroutineSetup), RpcTarget.AllViaServer);
    }

    [PunRPC]
    void RPCSetCoroutineSetup()
    {
        StartCoroutine(Setup());
    }

    //カードを生成して配る
    IEnumerator Setup()
    {   
        if (round == 1)
        {
            yield return new WaitForSeconds(2f);
            myRole = roleManager.GetRole(actorNum);
            // nipperPlayerNum = roleManager.GetFirstNipperNum();
            gameUI.ShowRole(myRole);
            yield return new WaitForSeconds(2f);//本当は5秒くらいにしたい
            gameUI.HideRoleDisPlay();
            cardGenarator.BuildCardList();
        }
        yield return new WaitForSeconds(2f);
        if (round >= 2)
        {
            RemoveCardFromPlayers();
        }
        gameSEManager.RoundnSE();
        gameUI.ShowRound(round);//ラウンドの表示
        yield return new WaitForSeconds(2f);
        gameUI.SetupNextRound();//ラウンドの非表示
        decideCard.OnDecideAction = DecidedAction;
        if (PhotonNetwork.IsMasterClient)
        {
            cardGenarator.CardShuffle();
            cardGenarator.SendCardListData();
            photonView.RPC(nameof(RPCSetCoroutineSendCard), RpcTarget.AllViaServer);
        }
        yield return new WaitForSeconds(3f);
        LampSet(true);
        declareObjs.SetActive(true);
        if (nipperPlayerNum == actorNum)
        {
            mouseManager.SetClosedNipper();
            gameUI.ShowYourTurn(myRole);
        }
    }
    IEnumerator SendCardToPlayers()
    {   
        yield return new WaitForSeconds(4f);
        for (int p_i = 0; p_i < players; p_i++)
        {
            for (int c_i = p_i*(6-round); c_i < (p_i+1)*(6-round); c_i++)
            {
                Card card = cardGenarator.Spawn(c_i, actorNum, p_i);//カード生成
                player[p_i].SetCardToHand(card);//カードを渡す
            }
            player[p_i].Hand.ResetPosition();//カードのポジションを整える
        }
    }
    void DecidedAction()//切断ボタンを押したら実行される
    {
        gameSEManager.NipperSE();
        mouseManager.SetClosedNipper();
        photonView.RPC(nameof(RPCSetCoroutineJudge), RpcTarget.AllViaServer, decideCard.selectedPlayerNum, decideCard.decidedCardTypeNum, decideCard.decidedCardNum);
    }
    void RemoveCardFromPlayers()
    {
        for (int p_i = 0; p_i < players; p_i++)
            {
                player[p_i].RemoveCardFromHand();//カードを破棄   
            }
    }
    IEnumerator JudgeCard(int selectedPlayerNum, int decidedCardTypeNum, int decidedCardNum)
    {
        StartCoroutine(gameSEManager.HeartBeatSE());
        yield return new WaitForSeconds(6f);//TODO秒数をランダムで決めたい
        Result result = ruleBook.GetResult(decidedCardTypeNum);
        preNipperPlayerNum = nipperPlayerNum;
        nipperPlayerNum = selectedPlayerNum;
        switch (result)
        {
            case Result.silent:
                gameUI.ShowTurnResult("しーん...");
                silent++;
                break;
            case Result.success:
                gameUI.ShowTurnResult("★解除★");
                gameSEManager.SuccessSE();
                success++;
                sum_success++;
                break;
            case Result.Boom:
                gameUI.ShowTurnResult("Boom!");
                gameSEManager.BombSE();
                Boom++;
                break;
        }
        yield return new WaitForSeconds(2.5f);
        if (sum_success==players)
        {
            ShowGameResult("POLICE");
        }
        else if (Boom==1)
        {
            ShowGameResult("BOMBER");
        }
        else
        {
            SetupNextTurn(decidedCardNum);
        }
    }

    void ShowGameResult(string winner)
    {
        declareObjs.SetActive(false);
        gameUI.ShowGameResult(winner);
    }
    void SetupNextTurn(int decidedCardNum)
    {
        if (preNipperPlayerNum == actorNum)
        {
            mouseManager.SetCursor();
            decideCard.SetupNextTurn();
        }
        else
        {
            int tmp_pNum = decidedCardNum/(6-round);
            int tmp_listNum = decidedCardNum%(6-round);
            player[tmp_pNum].FlipCard(tmp_listNum);
        }
        if (sum_success>0)lamp[sum_success-1].On();
        gameUI.SetupNextTurn();
        turnCount++;
        if (turnCount < players)
        {
            if (nipperPlayerNum == actorNum)
            {
                gameUI.ShowYourTurn(myRole);
                mouseManager.SetClosedNipper();
            }
        }
        else if (turnCount == players)
        {
            SetupNextRound();
        }
    }
    void SetupNextRound()
    {
        cardGenarator.RemoveBases(silent,success,Boom);
        round++;
        turnCount = 0;
        silent = success = Boom = 0;
        if (round==5)
        {
            ShowGameResult("BOMBER");
        }
        LampSet(false);
        declareObjs.SetActive(false);
        declare.SetupNextRound();
        StartCoroutine(Setup());
    }
    void SetAvatar()
    {
        PhotonNetwork.Instantiate("Avatar", new Vector3 (-0.5f, 1.3f, 0f), Quaternion.identity);
    }
    void SetPlayerPosition()
    {
        Vector3 tmp_player0Pos = player[0].transform.position;
        player[0].transform.position = player[actorNum].transform.position;
        player[actorNum].transform.position = tmp_player0Pos;
    }
    void LampSet(bool on)
    {
        for (int i=0; i < lamp.Count; i++)
        {
            lamp[i].gameObject.SetActive(on);
        }
    }
    public void OnRetryButton()
    {
        retryButton.SetActive(false);
        SendRetryMemberNum();
    }
    public void OnTitleButton()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
        FadeManager.Instance.LoadScene("Title", 1f);
    }
    public void OnExitButton()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
        FadeManager.Instance.LoadScene("Online", 1f);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        declareObjs.SetActive(false);
        gameUI.ShowLeavePanel();
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
    }
    public void SendRetryMemberNum()
    {
        var hashtable = new ExitGames.Client.Photon.Hashtable();
        retryHopeNum++;
        hashtable["Re"] = retryHopeNum;
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
        hashtable.Clear();
    }
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        retryHopeNum = (propertiesThatChanged["Re"] is int f_value) ? f_value : 0;
        gameUI.ShowRetryMessage(retryHopeNum);
        if (retryHopeNum == players)
        {
            FadeManager.Instance.LoadScene("Game", 1f);
        }
    }

    [PunRPC]
    void RPCSetCoroutineJudge(int selectedPlayerNum, int decidedCardTypeNum, int decidedCardNum)
    {
        StartCoroutine(JudgeCard(selectedPlayerNum, decidedCardTypeNum, decidedCardNum));
    }
    [PunRPC]
    void RPCSetCoroutineSendCard()
    {
        StartCoroutine(SendCardToPlayers());
    }

}
