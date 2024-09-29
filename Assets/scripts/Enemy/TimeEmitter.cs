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

    public Transform player;

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
            int attack = UnityEngine.Random.Range(0, 2);
            if (attack == 0)
            {
                Attack1();
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

    void Attack2(){
        StartCoroutine(Attack2Routine());
    }

    private IEnumerator Attack2Routine() {
        foreach (Transform firePoint in firePoints) {
            // fire 5 bullets towards the player
            for (int i = 0; i < 5; i++) {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position + new Vector3(0, 0, - 10), firePoint.rotation);
                bullet.GetComponent<Bullet>().lifeTime = 20f;
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null) {
                    Vector3 direction = player.position - firePoint.position;
                    rb.velocity = direction * bulletSpeed;
                }    
            }
        }
        yield return new WaitForSeconds(1f);
    }
}