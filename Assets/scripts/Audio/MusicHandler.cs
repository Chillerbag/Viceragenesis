using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioClip[] musicClips;
    public AudioSource musicSource;


    void Start()
    {
        musicSource.clip = musicClips[0];
        musicSource.Play();
    }

    public void changeMusic(int index)
    {
        musicSource.clip = musicClips[index];
        musicSource.Play();
    }

    public void changeVolume(float volume) {
        musicSource.volume = volume;
    }
}
