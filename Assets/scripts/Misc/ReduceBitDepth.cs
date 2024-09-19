using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceBitDepth : MonoBehaviour
{

    public float startValue = 10f;      // The value that will be reduced
    public float reductionDuration = 0.2f; // Duration over which the value is reduced
    public Material material;

    private Coroutine reductionCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            reductionCoroutine = StartCoroutine(ReduceValueOverTime());
        }
        // Stop any ongoing reduction coroutine before starting a new one
        if (Input.GetKeyDown(KeyCode.G))
        {
            //StopCoroutine(reductionCoroutine);
            material.SetFloat("_ColorResolution", startValue);
        }

        // Start the reduction coroutine

    }

    IEnumerator ReduceValueOverTime()
    {
        float elapsedTime = 0f;
        float value = startValue;

        while (elapsedTime < reductionDuration)
        {
            elapsedTime += Time.deltaTime;
            value = Mathf.Lerp(startValue, 0f, elapsedTime / reductionDuration);
            //value = Mathf.Clamp(value, 0f, 1f);
            material.SetFloat("_ColorResolution", value);
            yield return null; // Wait for the next frame
        }
    }
}
