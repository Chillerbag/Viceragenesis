using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flashEffect : MonoBehaviour
{

    public RawImage image;
    public float flashDuration = 0.1f;
    public Color flashColorDamage = new Color(1, 0, 0, 0.5f);

    public Color flashColorHeal = new Color(0, 1, 0, 0.5f);

    private void Start()
    {
        if (image != null)
        {
            image.color = Color.clear;
        }
    }

    public void FlashHurt()
    {
        if (image != null)
        {
            StartCoroutine(FlashRoutine(flashColorDamage));
        }
    }

    public void FlashHeal()
    {
        if (image != null)
        {
            StartCoroutine(FlashRoutine(flashColorHeal));
        }
    }

    private IEnumerator FlashRoutine(Color color)
    {
        image.color = color;
        yield return new WaitForSeconds(flashDuration);
        image.color = Color.clear;
    }
}
