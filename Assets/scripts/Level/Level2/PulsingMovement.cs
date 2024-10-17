using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingMovementWithCurve : MonoBehaviour
{
    public AnimationCurve pulseCurve;  // Define a curve in the Inspector
    public float frequency = 1f;
    public float amplitude = 1f;
    private Vector3 startPosition;

    public GameObject[] gameObjects;


    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {   
        bool isActive = false; 
        foreach (GameObject gameObject in gameObjects){
            if (! gameObject.GetComponent<EnemyHealthManager>().isDead){
                isActive = true;
            }
        }
        if ( isActive ){
            float curveValue = pulseCurve.Evaluate((Time.time * frequency) % 1f)*amplitude;
            transform.position = startPosition + new Vector3(0, curveValue, 0);

        } else{
            transform.position = startPosition;
        }
    }
}
