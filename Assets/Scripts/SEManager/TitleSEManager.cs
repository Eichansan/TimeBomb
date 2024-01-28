using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSEManager : MonoBehaviour
{
    [SerializeField] AudioClip decide;
    [SerializeField] AudioClip nipper;
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
    public IEnumerator HeartBeatSE()
    {
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(heartBeat);
    }
}
