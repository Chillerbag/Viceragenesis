using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatUpAndDown : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 0.1f;
    private float phaseOffset;
    void Start()
    {
        phaseOffset = Random.Range(0f, 0.5f * Mathf.PI);

        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void FixedUpdate() {
        transform.position += new Vector3(0, Mathf.Sin(Time.time + phaseOffset) * speed, 0);
    }
}
