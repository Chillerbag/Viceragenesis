using System;
using JetBrains.Annotations;
using UnityEngine;

public abstract class BossBehaviour : MonoBehaviour
{
    private Animator stateMachine;
    public int HP {get; set;}

    public string bossName {get; set;}

    public float attackCooldown {get; set;}

    public bool defeated = false;


    void OnEnable() {
        HP = 3;
    }

    public virtual void Attack() {
        // choose random int from 1 to 2 to determine which attack to use every 2 seconds, and choose between 1 3 if enraged
        int attackChoice = UnityEngine.Random.Range(1, 3);
        if (stateMachine.GetCurrentAnimatorStateInfo(0).IsName("BossEnrage")) {
            attackChoice = UnityEngine.Random.Range(1, 4);
        }
        if (attackChoice == 1) {
            stateMachine.SetTrigger("Attack1");
        } else if (attackChoice == 2) {
            stateMachine.SetTrigger("Attack2");
        } else {
            stateMachine.SetTrigger("Attack3");
        } 
    }
    
    public virtual void Damage(int dmg) {
        HP-=dmg;

        if (HP == 1) {
            // change state to enraged
            stateMachine.SetTrigger("Enraged");
        }

        if (HP<=0) {
            defeated = true;
            gameObject.SetActive(false);
        }
    }

    public virtual void EnrageBoss() 
    {
        // Change color to red
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red;
        }

        transform.localScale *= 1.5f; // Increase size by 50%
    }
}
