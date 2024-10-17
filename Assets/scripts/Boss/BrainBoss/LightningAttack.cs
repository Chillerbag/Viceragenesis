using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttack : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            // Try to get the PlayerHealth component from the collided object
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // If the component exists, apply damage
                playerHealth.TakeDamage(1);
            }
        }
    }
}
