using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatUpAndDown : MonoBehaviour
{
    // Start is called before the first frame update

    private float speed = 0.1f;
    void Start()
    {
        speed = Random.Range(0.08f, 0.15f);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        transform.position += new Vector3(0, Mathf.Sin(Time.time) * speed, 0);
    }
}
