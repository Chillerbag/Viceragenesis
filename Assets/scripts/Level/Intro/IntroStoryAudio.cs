using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStoryAudio : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FadeOut(float duration)
    {
        StartCoroutine(Fade(duration));
    }

    private IEnumerator Fade(float duration)
    {
        float startVolume = audioSource.volume;
        
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }
        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
