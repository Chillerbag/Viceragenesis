using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInCircle : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] private float radius = 10f;

    void Update()
    {
        float angle = Time.time * speed;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        Vector3 updatedPos = new Vector3(x, 0, z);
        transform.position = transform.position + updatedPos;
        
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}
