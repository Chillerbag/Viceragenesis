using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NeuronOscillation : MonoBehaviour
{
        public float rotationSpeed = 25f; 
        public float oscillationMagnitude = 0.007f;
        public float oscillationPeriod =2;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.up*math.cos(Time.time*oscillationPeriod)*oscillationMagnitude);
    }

}
