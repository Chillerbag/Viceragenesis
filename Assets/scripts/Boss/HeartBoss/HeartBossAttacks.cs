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

    public  GameObject attack2;
    public GameObject[] attack3;
    private AoeAttack aoeAttack; 
    public void Start(){
        stateMachine = GetComponent<Animator>();
        aoeAttack = attack2.GetComponent<AoeAttack>();
        startScale = attack3[0].transform.localScale;
        foreach(GameObject gameObject in attack3){
            Vector3 newScale = startScale;
            newScale.y = minSize;
            gameObject.transform.localScale = newScale;
        }
    }
    // shoot a spread of bullets in random semi-upward directions for 4s
    public void attack_1()
    {
        StartCoroutine(Attack1Routine());
    }

    private IEnumerator Attack1Routine() {
        for (int j = 0; j<5 ;j++){
            foreach (Transform firePoint in firePoints1) {
                    for (int i = 0; i < 360; i += 30)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                        bullet.GetComponent<BulletWithVarySpeed>().lifeTime = 10f;
                        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());

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
            }
            yield return new WaitForSeconds(0.15f);       

        }


        yield return new WaitForSeconds(0.5f);       
        for (int j = 0; j<5 ;j++){

            foreach (Transform firePoint in firePoints1) {
                    for (int i = 15; i < 390; i += 30)
                    {
                        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                        bullet.GetComponent<BulletWithVarySpeed>().lifeTime = 10f;
                        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());

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
                }
            yield return new WaitForSeconds(0.15f);       

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
        Debug.Log("Start Attack");
        aoeAttack.active = true;
        yield return null;
        while(aoeAttack.isAttacking){
            aoeAttack.active = false;
            yield return null;
        }
        stateMachine.SetTrigger("returnIdleAfterAttack");
        stateMachine.ResetTrigger("Attack2");

    }

    
    private Vector3 startScale;
    public float maxSize = 2f;

    private float currentSize;
    public float increaseSpeed = 1f;

    public float stopTime = 0.75f;
    
    public float minSize =0.01f;
    public void attack_3()
    {       
        Debug.Log("attack3");
            StartCoroutine(Attack3Routine());
    }

    private IEnumerator Attack3Routine()
    {
            while (currentSize < maxSize){
                foreach(GameObject gameObject in attack3)
                {

                currentSize += increaseSpeed*Time.deltaTime ;
                Vector3 newScale = startScale;
                newScale.y = startScale.y * currentSize;
                gameObject.transform.localScale =newScale;
                }
                yield return null;
        }
        yield return new WaitForSeconds(stopTime);

        while (currentSize > minSize){
                foreach(GameObject gameObject in attack3)
        {
            currentSize -= increaseSpeed*Time.deltaTime ;
            Vector3 newScale = startScale;
            newScale.y = startScale.y * currentSize;
            gameObject.transform.localScale =newScale;

             yield return null;
        }
        }
        currentSize = minSize;
        yield return new WaitForSeconds(stopTime);

        stateMachine.SetTrigger("returnIdleAfterAttack");
        stateMachine.ResetTrigger("Attack3");

    }


      
}