using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAttack : MonoBehaviour, IAttackable
{
    public Transform player; // Reference to the player's transform
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public float bulletSpeed = 1f; 

    public void Attack()
    {
        // Instantiate a bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Set the bullet's velocity to only move on the flat axis
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 flatDirection = (player.position - transform.position).normalized;
            flatDirection.y = 0; // Ensure the bullet only travels on the flat axis
            rb.velocity = flatDirection * bulletSpeed;
        }
    }
}

