using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public Transform player = null; // Reference to the player's transform
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    public float shootingInterval = 2f; 
    public float bulletSpeed = 1f; 
    private float shootingTimer;

    void Update()
    {
        
        FacePlayer();

        
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0f)
        {
            Attack();
            shootingTimer = shootingInterval;
        }
    }

    void FacePlayer()
    {
        
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Ignore the vertical difference

        // Rotate towards the player on the horizontal plane
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

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

