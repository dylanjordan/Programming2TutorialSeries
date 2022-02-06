using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Background audio source
    public AudioSource _audioPlayer;
    //Background music
    public AudioClip backgroundMusic;

    // Start is called before the first frame update
    void Awake()
    {
        //Set the audio player to background music
        _audioPlayer.clip = backgroundMusic;
        //Play music
        _audioPlayer.Play();
    }
}
