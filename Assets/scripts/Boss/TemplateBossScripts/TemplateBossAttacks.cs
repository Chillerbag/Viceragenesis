using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
     public Transform player; // Reference to the player's transform
    public GameObject bulletPrefab; 
    public Transform firePoint; 

    private Animator stateMachine;
    
    private float bulletSpeed = 10f; 

    // shoot a stream of bullets at the player for 4s
    public void attack_1()
    {
        StartCoroutine(Attack1Routine());
    }

     private IEnumerator Attack1Routine() {
        stateMachine = GetComponent<Animator>();
        for (int i = 0; i < 100; i++) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("bullet created");
            // Set the bullet's velocity
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (player.position - firePoint.position).normalized;
                rb.velocity = direction * bulletSpeed;
            }
            else
            {
                Debug.LogError("Rigidbody component not found on bulletPrefab.");
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
            for (int i = 0; i < 360; i += 10)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                Debug.Log("bullet created");
                bullet.transform.Rotate(0, i, 0);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = bullet.transform.forward;
                    rb.velocity = direction * bulletSpeed;
                }
                else
                {
                    Debug.LogError("Rigidbody component not found on bulletPrefab.");
                }
            }
            // wait for 1 second before shooting the next ring
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
            else
            {
                Debug.LogError("Rigidbody component not found on bulletPrefab.");
            }

            currentAngle += angleIncrement;
            currentRadius += radiusIncrement;

            // Wait for a short duration before spawning the next bullet
            yield return new WaitForSeconds(0.05f);
        }
    }   
}

