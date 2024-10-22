using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDamage : MonoBehaviour
{
    public void OnTriggerEnter(Collider collision) {

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
