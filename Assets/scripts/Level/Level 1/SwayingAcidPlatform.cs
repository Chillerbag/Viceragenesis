using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayingAcidPlatform : MonoBehaviour
{
    public float bobbingAmplitude = 0.5f;
    public float bobbingFrequency = 1.0f;
    public float swayingAmplitude = 15.0f;
    public float swayingFrequency = 1.0f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Bobbing effect
        float bobbingOffset = Mathf.Sin(Time.time * bobbingFrequency) * bobbingAmplitude;
        transform.position = initialPosition + new Vector3(0, bobbingOffset, 0);

        // Swaying effect
        float swayingOffset = Mathf.Sin(Time.time * swayingFrequency) * swayingAmplitude;
        transform.rotation = initialRotation * Quaternion.Euler(0, 0, swayingOffset);
    }
}