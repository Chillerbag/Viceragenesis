using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // method for other scripts
    public void PlayAudio()
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }

    // method for other scripts
    public void StopAudio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
