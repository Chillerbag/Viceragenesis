using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainBossAttacks : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject bulletPrefab; 
    public Transform[] firePoints; // Array of fire points
    public GameObject[] lightningStrikes; // Array of lightning strikes

    public GameObject turningLaser;
    private Animator stateMachine;
    private float bulletSpeed = 20f; 

    public GameObject cuttingLaser;

    public void attack_1()
    {
        StartCoroutine(Attack1Routine());
    }

    private IEnumerator Attack1Routine() {
        stateMachine = GetComponent<Animator>();
        // choose a random 10 lightning strikes, and strike them at different times
        for (int i = 0; i < 3; i++) {
            int randomIndex = Random.Range(0, lightningStrikes.Length);
            LightningStrike lightningStrike = lightningStrikes[randomIndex].GetComponent<LightningStrike>();
            lightningStrike.StrikeLightning();
            yield return new WaitForSeconds(0.5f);
        }
        stateMachine.SetTrigger("returnIdleAfterAttack");
        stateMachine.ResetTrigger("Attack1");
    }

    public void attack_2() {
        StartCoroutine(Attack2Routine());
    }

    private IEnumerator Attack2Routine() {
        stateMachine = GetComponent<Animator>();
        turningLaser.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        stateMachine.SetTrigger("returnIdleAfterAttack");
        stateMachine.ResetTrigger("Attack2");

    }

    public void attack_3() {
        StartCoroutine(Attack3Routine());
    }

    private IEnumerator Attack3Routine() {
        stateMachine = GetComponent<Animator>();
        cuttingLaser.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        stateMachine.SetTrigger("returnIdleAfterAttack");
        stateMachine.ResetTrigger("Attack3");
    }

    public void attack_4() {
        StartCoroutine(Attack4Routine());
    }

    private IEnumerator Attack4Routine()
    {
        stateMachine = GetComponent<Animator>();
        int wavesCount = 3;
        float waveDuration = 0.5f;
        float pulseFactor = 0.5f;

        for (int wave = 0; wave < wavesCount; wave++)
        {
            float elapsedTime = 0f;
            while (elapsedTime < waveDuration)
            {
                float intensity = Mathf.Sin(elapsedTime / waveDuration * Mathf.PI) * pulseFactor + 1f;
                
                for (int i = 0; i < firePoints.Length; i++)
                {
                    GameObject bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    
                    Vector3 randomSpread = new Vector3(
                        Random.Range(-0.1f, 0.1f),
                        Random.Range(-0.1f, 0.1f),
                        Random.Range(-0.1f, 0.1f)
                    );
                    
                    Vector3 direction = (firePoints[i].forward + randomSpread).normalized;
                    rb.AddForce(direction * bulletSpeed * intensity, ForceMode.Impulse);
                }

                elapsedTime += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);
        stateMachine.SetTrigger("returnIdleAfterAttack");
        stateMachine.ResetTrigger("Attack4");
    }

    public void attack_5() {
        StartCoroutine(Attack5Routine());
    }

    private IEnumerator Attack5Routine()
{
    stateMachine = GetComponent<Animator>();
    float spiralDuration = 3f;
    float spiralSpeed = 720f; // Degrees per second
    float bulletFireRate = 0.1f;
    float elapsedTime = 0f;

    while (elapsedTime < spiralDuration)
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            float angle = (elapsedTime * spiralSpeed) + (360f / firePoints.Length * i);
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            GameObject bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation * rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bullet.transform.forward * bulletSpeed, ForceMode.Impulse);
        }

        elapsedTime += bulletFireRate;
        yield return new WaitForSeconds(bulletFireRate);
    }

    yield return new WaitForSeconds(0.5f);
    stateMachine.SetTrigger("returnIdleAfterAttack");
    stateMachine.ResetTrigger("Attack5");
}






}