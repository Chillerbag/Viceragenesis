using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceBitDepth : MonoBehaviour
{

    public float startValue = 10f;      // The value that will be reduced
    public float reductionDuration = 0.2f; // Duration over which the value is reduced
    public Material material;

    private Coroutine reductionCoroutine;

    void start(){
        material.SetFloat("_ColorResolution", startValue);

    }
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
        material.SetFloat("_ColorResolution", startValue);
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
