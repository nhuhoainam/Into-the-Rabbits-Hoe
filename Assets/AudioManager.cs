using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource; 
    public AudioSource sfxAudioSource;
    public AudioClip musicClip;
    public AudioClip runningClip;
    
    // Start is called before the first frame update
    void Start()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }

   
}
