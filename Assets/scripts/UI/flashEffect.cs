using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flashEffect : MonoBehaviour
{

    public RawImage image;
    public float flashDuration = 0.1f;
    public Color flashColor = new Color(1, 0, 0, 0.5f);

    private void Start()
    {
        if (image != null)
        {
            image.color = Color.clear;
        }
    }

    public void Flash()
    {
        if (image != null)
        {
            StartCoroutine(FlashRoutine());
        }
    }

    private IEnumerator FlashRoutine()
    {
        image.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        image.color = Color.clear;
    }
}
