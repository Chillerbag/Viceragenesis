using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PulsingMovementWithCurve : Bouncing
{
    public AnimationCurve pulseCurve;  // Define a curve in the Inspector
    public float frequency = 1f;
    public float amplitude = 1f;
    private Vector3 startPosition;

    public GameObject[] gameObjects;


    void Start()
    {
        isActive = false; 
        startPosition = transform.position;
    }

    void Update()
    {   
        bool flag = false;
        foreach (GameObject gameObject in gameObjects){
            if (! gameObject.GetComponent<EnemyHealthManager>().isDead){
                
                flag = true;
            }
        }
        if (flag){
            float curveValue = pulseCurve.Evaluate((Time.time * frequency) % 1f)*amplitude;
            transform.position = startPosition + new Vector3(0, curveValue, 0);
            isActive = true;

        } else{
            transform.position = startPosition;
            isActive = false;

        }
    }
}
