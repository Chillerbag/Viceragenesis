using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// code mainly follows Sasquatch B Studios sound effects in unity tutorial, citing link for reference:
// https://www.youtube.com/watch?v=DU7cgVsU2rM

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", level);
    }
    
    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", level);
    }

    public void SetBackgroundMusicVolume(float level)
    {
        audioMixer.SetFloat("BackgroundMusicVolume", level);
    }
}
