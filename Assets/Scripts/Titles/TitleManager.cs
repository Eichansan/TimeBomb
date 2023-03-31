using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TitleManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject decideButton;
    [SerializeField] InputField nameInputField;
    private string nickName;
    private void Start() 
    {
        startButton.SetActive(false);   
    }
    public void InputText()
    {
        if (nameInputField.text != "")
        {
            nickName = nameInputField.text;
            PhotonNetwork.NickName = nickName;
            nameInputField.gameObject.SetActive(false);
            decideButton.SetActive(false);
            startButton.SetActive(true);
        }
    }
    public void OnStart()
    {
        FadeManager.Instance.LoadScene("Online", 1f);
    }
}
