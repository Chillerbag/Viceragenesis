using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEmitter : MonoBehaviour
{
    // todo: fix the different patterns. 
    public GameObject bulletPrefab; 
    public Transform[] firePoints; 
    public float shootingInterval = 5f; 
    public float bulletSpeed = 10f; 
    private float shootingTimer;

    public Transform player;

    void Start()
    {
        
        shootingTimer = shootingInterval;
        shootingTimer = 0f;
    }

    void Update()
    {
        // Handle shooting at intervals
        shootingTimer -= Time.deltaTime;

        // every 5 seconds, choose a new attack
        if (shootingTimer <= 0f)
        {
            int attack = UnityEngine.Random.Range(0, 2);
            if (attack == 0)
            {
                Attack2();
                shootingTimer = shootingInterval;
            }
            if (attack == 1)
            {
                Attack2();
                shootingTimer = shootingInterval;
            }
        }
    }

    void Attack1(){
        StartCoroutine(Attack1Routine());
    }

    private IEnumerator Attack1Routine() {
        Vector3[] BulletLocs;
        BulletLocs = new Vector3[] { new Vector3(0, 0, 0), new Vector3(-5, 0, 0), new Vector3(-10, 0, 0), new Vector3(-15, 0, 0), new Vector3(-20, 0, 0), new Vector3(-25, 0, 0), new Vector3(5, 0, 0), new Vector3(10, 0, 0), new Vector3(15, 0, 0), new Vector3(20, 0, 0) };
        foreach (Transform firePoint in firePoints) {
            print(firePoint);
            for (int i = 0; i < 9; i++) {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position + new Vector3(0, 0, - 10) + BulletLocs[i], firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null) {
                    Vector3 flatDirection = (player.position - transform.position).normalized;
                    rb.velocity = flatDirection * bulletSpeed;
                } else {
                    Debug.LogError("Rigidbody component not found on bulletPrefab.");
                }
            }
            yield return new WaitForSeconds(3f); 
        }
    }

    void Attack2(){
        StartCoroutine(Attack2Routine());
    }

    private IEnumerator Attack2Routine() {
        foreach (Transform firePoint in firePoints) {
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
    }
}