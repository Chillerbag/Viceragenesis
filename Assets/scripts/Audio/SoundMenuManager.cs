using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenuManager : MonoBehaviour
{
    public GameObject soundMenuCanvas;

    private bool isSoundMenuVisible = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleSoundMenuCanvas();
        }
    }

    private void ToggleSoundMenuCanvas()
    {
        isSoundMenuVisible = !isSoundMenuVisible;
        soundMenuCanvas.SetActive(isSoundMenuVisible);
    }
}
