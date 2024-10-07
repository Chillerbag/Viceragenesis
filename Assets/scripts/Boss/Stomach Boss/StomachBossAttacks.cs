using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomachBossAttacks : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject bulletPrefab; 
    public Transform[] firePoints; // Array of fire points

    private Animator stateMachine;
    
    private float bulletSpeed = 10f; 

    // shoot a spread of bullets in random semi-upward directions for 4s
    public void attack_1()
    {
        StartCoroutine(Attack1Routine());
    }

    private IEnumerator Attack1Routine() {
        stateMachine = GetComponent<Animator>();
        for (int i = 0; i < 100; i++) {
            foreach (Transform firePoint in firePoints)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                // Set the bullet's velocity
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Generate a random direction semi-upward
                    Vector3 randomDirection = new Vector3(
                        Random.Range(-1f, 1f),
                        Random.Range(0.5f, 1f), // Ensure the y component is upward
                        Random.Range(-1f, 1f)
                    ).normalized;
                    rb.velocity = randomDirection * bulletSpeed;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }

        stateMachine.SetTrigger("returnIdleAfterAttack");
        stateMachine.ResetTrigger("Attack1");
    }

    // shoot bullets in a ring shape 3 times
    public void attack_2()
    {
        StartCoroutine(Attack2Routine());
    }

    private IEnumerator Attack2Routine() {
    stateMachine = GetComponent<Animator>();
    for (int j = 0; j < 3; j++)
    {
        foreach (Transform firePoint in firePoints)
        {
            for (int i = 0; i < 5; i++) // Fire a few bullets towards the player
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Calculate direction towards the player
                    Vector3 direction = (player.position - firePoint.position).normalized;
                    rb.velocity = direction * bulletSpeed;
                }
            }
        }
        // wait for 1 second before shooting the next set of bullets
        yield return new WaitForSeconds(1f);
    }

        stateMachine.SetTrigger("returnIdleAfterAttack");
        stateMachine.ResetTrigger("Attack2");
    }

    public void attack_3()
    {
        StartCoroutine(Attack3Routine());
    }

    private IEnumerator Attack3Routine()
    {
        stateMachine = GetComponent<Animator>();
        int numberOfBullets = 100; // Total number of bullets to be fired
        float angleIncrement = 10f; // Angle increment in degrees
        float radiusIncrement = 0.1f; // Radius increment per bullet

        float currentAngle = 0f;
        float currentRadius = 0f;

        foreach (Transform firePoint in firePoints)
        {
            for (int i = 0; i < numberOfBullets; i++)
            {   
                float x = currentRadius * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
                float z = currentRadius * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
                Vector3 bulletPosition = new Vector3(x, 0, z) + firePoint.position;

                GameObject bullet = Instantiate(bulletPrefab, bulletPosition, firePoint.rotation);
                bullet.transform.LookAt(firePoint.position);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = bullet.transform.forward;
                    rb.velocity = direction * bulletSpeed;
                }


                currentAngle += angleIncrement;
                currentRadius += radiusIncrement;

                // Wait for a short duration before spawning the next bullet
                yield return new WaitForSeconds(0.05f);
            }
        }
    }   
}