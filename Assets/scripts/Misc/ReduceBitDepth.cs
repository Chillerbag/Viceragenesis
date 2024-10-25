using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;


public class ReduceBitDepth : MonoBehaviour
{

    public float startValue = 1f;      // The value that will be reduced
    public float reductionDuration = 1f; // Duration over which the value is reduced
    public RawImage image;
    private Coroutine reductionCoroutine;


    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            reductionCoroutine = StartCoroutine(ReduceValueOverTime());
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            material.SetFloat("_ColorResolution", startValue);
        }
        */
    }

    public void ReduceScreenBitDepth()
    {
        StartCoroutine(ReduceValueOverTime());
    }

    public void ResetScreenBitDepth()
    {
        StartCoroutine(IncreaseValueOverTime());
    }

    IEnumerator ReduceValueOverTime()
    {
        float elapsedTime = 0f;
        float value = startValue;

        while (elapsedTime < reductionDuration)
        {
            elapsedTime += Time.deltaTime;
            value = Mathf.Lerp(startValue, 0f, elapsedTime / reductionDuration);
            value = Mathf.Clamp(value, 0f, 1f);

            image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - value);
            //material.SetFloat("_ColorResolution", value);
            yield return null; // Wait for the next frame
        }
    }

    IEnumerator IncreaseValueOverTime()
    {
        float elapsedTime = 0f;
        float value = startValue;

        while (elapsedTime < reductionDuration)
        {
            elapsedTime += Time.deltaTime;
            value = Mathf.Lerp(0f, startValue, elapsedTime / reductionDuration);
            value = Mathf.Clamp(value, 0f, 1f);

            image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - value);
            //material.SetFloat("_ColorResolution", value);
            yield return null; // Wait for the next frame
        }
    }
}
