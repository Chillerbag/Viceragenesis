using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDirectionAttack: MonoBehaviour, IAttackable
{
    public GameObject bulletPrefab; 
    public Transform[] firePoints; 
    public float bulletSpeed = 5f; 


    public void Attack()
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

