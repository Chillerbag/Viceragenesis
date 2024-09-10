using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// code mainly follows Sasquatch B Studios sound effects in unity tutorial, citing link for reference:
// https://www.youtube.com/watch?v=DU7cgVsU2rM
public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform soundLocation, float volume)
    {
        // spawn the audio source as soundFXObject at sound location (rotation does not matter)
        AudioSource audioSource = Instantiate(soundFXObject, soundLocation.position, Quaternion.identity);

        // assign the clip, volume and the clip length
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;

        //Debug.Log("PLAYING SOUND AT " + soundLocation.position);
        //Debug.Log("PLAYING SOUND " + audioClip.name);

        // destroy the soundFXObject when sound is done playing
        Destroy(audioSource.gameObject, clipLength);

    }

    // method that randomly chooses a sound fx from array given in inspector
    public void PlayRandomSoundFXClip(AudioClip[] audioClips, Transform soundLocation, float volume)
    {
        int indexChosen = Random.Range(0, audioClips.Length); 

        // spawn the audio source as soundFXObject at sound location (rotation does not matter)
        AudioSource audioSource = Instantiate(soundFXObject, soundLocation.position, Quaternion.identity);

        // assign the clip, volume and the clip length
        audioSource.clip = audioClips[indexChosen];
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        // destroy the soundFXObject when sound is done playing
        Destroy(audioSource.gameObject, clipLength);

    }
}
