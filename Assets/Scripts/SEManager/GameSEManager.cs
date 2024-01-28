using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSEManager : MonoBehaviour
{
    [SerializeField] AudioClip decide;
    [SerializeField] AudioClip nipper;
    [SerializeField] AudioClip cardSelect;
    [SerializeField] AudioClip pmButton;
    [SerializeField] AudioClip roundSE;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip bomb;
    [SerializeField] AudioClip heartBeat;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnDecideButtonSE()
    {
        audioSource.PlayOneShot(decide);
    }
    public void NipperSE()
    {
        audioSource.PlayOneShot(nipper);
    }
    public void CardSelectSE()
    {
        audioSource.PlayOneShot(cardSelect);
    }
    public void PMButtonSE()
    {
        audioSource.PlayOneShot(pmButton);
    }
    public void RoundnSE()
    {
        audioSource.PlayOneShot(roundSE);
        VolumeChange();
    }
    public void SuccessSE()
    {
        audioSource.PlayOneShot(success);
    }
    public void BombSE()
    {
        audioSource.PlayOneShot(bomb);
    }
    public IEnumerator HeartBeatSE()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(heartBeat);
    }
    public void VolumeChange()
    {
        StartCoroutine(VolumeDown());
    }
    
    IEnumerator VolumeDown()
    {
        while(audioSource.volume > 0)
        {
            audioSource.volume -=0.02f;
            yield return new WaitForSeconds(0.1f);
        }
        if(audioSource.volume == 0)
        {
            audioSource.Stop();
            audioSource.volume = 1f;
        }
    }
}
