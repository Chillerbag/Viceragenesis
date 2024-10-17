using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainBossAttacks : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject bulletPrefab; 
    public Transform[] firePoints; // Array of fire points
    public GameObject[] lightningStrikes; // Array of lightning strikes
    private Animator stateMachine;
    private float bulletSpeed = 10f; 

    public void attack_1()
    {
        StartCoroutine(Attack1Routine());
    }

    private IEnumerator Attack1Routine() {
        stateMachine = GetComponent<Animator>();
        // choose a random 10 lightning strikes, and strike them at different times
        for (int i = 0; i < 30; i++) {
            int randomIndex = Random.Range(0, lightningStrikes.Length);
            LightningStrike lightningStrike = lightningStrikes[randomIndex].GetComponent<LightningStrike>();
            lightningStrike.StrikeLightning();
            yield return new WaitForSeconds(0.5f);
        }
        stateMachine.SetTrigger("returnIdleAfterAttack");
        stateMachine.ResetTrigger("Attack1");
    }
}