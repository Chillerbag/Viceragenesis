using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnzyme : MonoBehaviour
{
    // Speed of rotation
    public float rotationSpeed = 100f;
    public Vector3 rotateVector;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateVector * rotationSpeed * Time.deltaTime);
    }
}
