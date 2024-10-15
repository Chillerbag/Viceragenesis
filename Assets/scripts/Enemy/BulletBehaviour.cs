using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    private static List<Bullet> bullets = new List<Bullet>();

    void Start()
    {
        bullets.Add(this);
        if (bullets.Count > 200)
        {
            Destroy(bullets[0].gameObject);
            bullets.RemoveAt(0);
        }
        // Schedule the bullet to be destroyed after 10 seconds
        Invoke("DestroyBullet", 10f);
    }

    void OnDestroy()
    {
        bullets.Remove(this);
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
        if (collision.gameObject.tag == "Boss")
        {
            // don't collide with boss
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        if (collision.gameObject.tag == "ArenaWall")
        {
            // don't collide with arena walls
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Boss")
        {
            Destroy(gameObject);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}