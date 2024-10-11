using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartBossAttacks : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject bulletPrefab; 
    public Transform[] firePoints1; // Array of fire points

    private Animator stateMachine;
    
    private float bulletSpeed = 10f; 

    public void Start(){
        stateMachine = GetComponent<Animator>();

    }
    // shoot a spread of bullets in random semi-upward directions for 4s
    public void attack_1()
    {
        StartCoroutine(Attack1Routine());
    }

    private IEnumerator Attack1Routine() {
        foreach (Transform firePoint in firePoints1) {
                for (int i = 0; i < 360; i += 30)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    bullet.GetComponent<Bullet>().lifeTime = 10f;

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
                yield return null;        
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
        foreach (Transform firePoint in firePoints1) {
                for (int i = 0; i < 360; i += 30)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    bullet.GetComponent<Bullet>().lifeTime = 10f;

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
                yield return null;
        }

        stateMachine.SetTrigger("returnIdleAfterAttack");
        stateMachine.ResetTrigger("Attack1");
    }

    public void attack_3()
    {
        StartCoroutine(Attack3Routine());
    }

    private IEnumerator Attack3Routine()
    {
        yield return null;
    }   
}