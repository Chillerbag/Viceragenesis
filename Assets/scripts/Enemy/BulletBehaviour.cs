using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1; 
    public float lifeTime = 3f; 
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
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
        if (collision.gameObject.tag == "Boss") {
            // dont collide with boss
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        if (collision.gameObject.tag == "ArenaWall") {
            // dont collide with arena walls
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Boss") {
            Destroy(gameObject);
        }
    }
}