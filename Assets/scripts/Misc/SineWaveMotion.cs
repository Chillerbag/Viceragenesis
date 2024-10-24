using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveMotion : MonoBehaviour
{
    public float amplitude = 1f; // Height of the wave
    public float frequency = 1f; // Speed of the wave

    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Calculate the new Y position based on sine wave
        float newY = Mathf.Sin(Time.time * frequency) * amplitude;

        // Update the object's position
        transform.position += new Vector3(0, newY, 0);
    }
}
