using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEmitter : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform[] firePoints; 
    public float shootingInterval = 20f; 
    public float bulletSpeed = 20f; 
    private float shootingTimer;

    void Start()
    {
        
        shootingTimer = shootingInterval;
    }

    void Update()
    {
        // Handle shooting at intervals
        shootingTimer -= Time.deltaTime;

        // every 5 seconds, choose a new attack
        if (shootingTimer <= 0f)
        {
            int attack = UnityEngine.Random.Range(0, 3);
            if (attack == 0)
            {
                Attack1();
                shootingTimer = shootingInterval;
            }
            if (attack == 1)
            {
                Attack1();
                shootingTimer = shootingInterval;
            }
            if (attack == 2)
            {
                Attack1();
                shootingTimer = shootingInterval;
            }
        }
    }

    void Attack1(){
        StartCoroutine(Attack1Routine());
    }

    private IEnumerator Attack1Routine() {
        Vector3[] BulletLocs;
        BulletLocs = new Vector3[] { new Vector3(0, 0, 0), new Vector3(-5, 0, 0), new Vector3(-10, 0, 0), new Vector3(-15, 0, 0), new Vector3(-20, 0, 0), new Vector3(5, 0, 0), new Vector3(10, 0, 0), new Vector3(15, 0, 0), new Vector3(20, 0, 0) };
        foreach (Transform firePoint in firePoints) {
            print(firePoint);
            for (int i = 0; i < 9; i++) {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position + new Vector3(0, 0, - 10) + BulletLocs[i], firePoint.rotation);
                bullet.GetComponent<Bullet>().lifeTime = 20f;
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null) {
                    rb.velocity = firePoint.forward * bulletSpeed;
                } else {
                    Debug.LogError("Rigidbody component not found on bulletPrefab.");
                }
            }
            yield return new WaitForSeconds(1f); 
        }
    }
}
