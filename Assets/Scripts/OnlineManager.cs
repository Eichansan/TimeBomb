using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class OnlineManager : MonoBehaviourPunCallbacks
{
    [SerializeField] OnlineSEManager onlineSEManager;
    [SerializeField] GameObject loadingAnim;
    [SerializeField] GameObject matchingMessage;
    [SerializeField] GameObject matchingButton;
    [SerializeField] GameObject sixPlayersButton;
    [SerializeField] GameObject sevenPlayersButton;
    [SerializeField] GameObject eightPlayersButton;
    bool isMatching;

    private void Start() 
    {
        GameDataManager.Instance.inRoom = false;
        matchingButton.SetActive(false);
    }
    public void OnMatchingButton() 
    {
        onlineSEManager.OnDecideButtonSE();
        loadingAnim.SetActive(true);
        matchingMessage.SetActive(true);
        matchingButton.SetActive(false); 
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster() 
    {
        PhotonNetwork.JoinRandomRoom();
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        // PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom() 
    {
        GameDataManager.Instance.inRoom = true;
    }
  // ゲームサーバーへの接続が失敗した時に呼ばれるコールバック
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 1}, TypedLobby.Default);
    }
    private void Update()
    {
        if (isMatching)
        {
            return;
        }
        if (GameDataManager.Instance.inRoom)
        {
            if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                isMatching = true;
                FadeManager.Instance.LoadScene ("Game", 1.0f);
            }               
        }
    } 

    public void OnSixPlayersButton() 
    {
        GameDataManager.Instance.players = 6;
        ButtonsSetInactive();
        matchingButton.SetActive(true); 
        onlineSEManager.OnDecideButtonSE();
    }
    public void OnSevenPlayersButton() 
    {
        GameDataManager.Instance.players = 7;
        ButtonsSetInactive();
        matchingButton.SetActive(true); 
        onlineSEManager.OnDecideButtonSE();
    }
    public void OnEightPlayersButton() 
    {
        GameDataManager.Instance.players = 8;
        ButtonsSetInactive();
        matchingButton.SetActive(true); 
        onlineSEManager.OnDecideButtonSE();
    }
    void ButtonsSetInactive() 
    {
        sixPlayersButton.SetActive(false);
        sevenPlayersButton.SetActive(false);
        eightPlayersButton.SetActive(false);
    }
}