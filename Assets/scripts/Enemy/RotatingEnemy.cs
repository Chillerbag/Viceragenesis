using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemy : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform[] firePoints; 
    public float rotationSpeed = 25f; 
    public float shootingInterval = 1f; 
    public float bulletSpeed = 5f; 
    private float shootingTimer;

    void Start()
    {
        
        shootingTimer = shootingInterval;
    }

    void Update()
    {
        // Rotate the enemy slowly around the Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Handle shooting at intervals
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0f)
        {
            ShootInAllDirections();
            shootingTimer = shootingInterval;
        }
    }

    void ShootInAllDirections()
    {
        // Shoot bullets from each fire point
        foreach (Transform firePoint in firePoints)
        {
            if (firePoint != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = firePoint.forward * bulletSpeed;
                }
            }
        }
    }
}

