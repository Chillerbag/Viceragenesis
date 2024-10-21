using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingStrech : Bouncing
{   
    public bool isBeating = false;
    public AnimationCurve pulseCurve;  // Define a curve in the Inspector
    public float frequency = 1f;
    public float amplitude = 1f;
    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {   
        if (isBeating & active){
            float curveValue = pulseCurve.Evaluate((Time.time * frequency) % 1f)*amplitude;
            transform.localScale = new Vector3(startScale.x, startScale.y*(1+ curveValue), startScale.z);
        } else{
            transform.localScale = startScale;
        }
    }
}
