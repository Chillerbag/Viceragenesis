using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
     public Transform player; // Reference to the player's transform
    public GameObject bulletPrefab; 
    public Transform firePoint; 

    private Animator stateMachine;
    
    private float bulletSpeed = 3f; 

    // shoot a stream of bullets at the player for 4s
    public void attack_1()
    {
        float attackDuration = 5f;
        while (attackDuration > 0) {
            attackDuration -= Time.deltaTime;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // Set the bullet's velocity
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = player.position * bulletSpeed;
            }
            else
            {
                Debug.LogError("Rigidbody component not found on bulletPrefab.");
            }
        }
        if (stateMachine.GetCurrentAnimatorStateInfo(0).IsName("BossEnrage")) {
            stateMachine.SetTrigger("returnToEnragedAfterAttack");
        } else {
            stateMachine.SetTrigger("returnIdleAfterAttack");
        }
    }

    // shoot bullets in a ring shape 3 times
    public void attack_2()
    {
        for (int j = 0; j < 3; j++)
        { 
            for (int i = 0; i < 360; i += 10)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.transform.Rotate(0, i, 0);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                Vector3 direction = bullet.transform.forward;
                rb.velocity = direction * bulletSpeed;
            } 
            // wait for 1s before shooting the next ring
            float ringTimer = 1f;
            while (ringTimer > 0)
            {
                ringTimer -= Time.deltaTime;
            }
        }
        if (stateMachine.GetCurrentAnimatorStateInfo(0).IsName("BossEnrage")) {
            stateMachine.SetTrigger("returnToEnragedAfterAttack");
        } else {
            stateMachine.SetTrigger("returnIdleAfterAttack");
        }
    }


    public void attack_3()
    {
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

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.transform.position = bulletPosition;
            bullet.transform.LookAt(firePoint.position);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            Vector3 direction = bullet.transform.forward;
            rb.velocity = direction * bulletSpeed;

            currentAngle += angleIncrement;
            currentRadius += radiusIncrement;
        }
        stateMachine.SetTrigger("returnToEnragedAfterAttack");
    }








}

