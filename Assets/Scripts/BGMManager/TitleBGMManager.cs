using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGMManager : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void StopBGM()
    {
        audioSource.Stop();
    }
}
