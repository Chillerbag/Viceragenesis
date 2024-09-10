using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletFollowsPlayer : MonoBehaviour
{
    public int damage = 1; 
    public float lifeTime = 10f; 
    public Transform Player;
    public float maxTurnSpeed = 5f; // degrees per second

    public float bulletSpeed = 100f;
    public Rigidbody rb;
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
        rb.velocity = new Vector3(0,0,0);
        Player =  GameObject.Find("Player").transform;

    }

    void Update()
    {
       
          

            Vector3 direction = Player.position - rb.position;

            direction.Normalize();

            Vector3 rotateAmount = Vector3.Cross(direction, transform.forward);

            rb.angularVelocity = -rotateAmount * maxTurnSpeed;

            rb.velocity = transform.forward * bulletSpeed;

    


    }
    void OnCollisionEnter(Collision collision)
    {
        // Try to get the PlayerHealth component from the collided object
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // If the component exists, apply damage
            playerHealth.TakeDamage(damage);
        }

        Destroy(gameObject);

    }
}