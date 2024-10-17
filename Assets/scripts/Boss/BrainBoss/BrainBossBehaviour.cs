using System;
using JetBrains.Annotations;
using UnityEngine;

public class BrainBossBehaviour : BossBehaviour
{
    private Animator stateMachine;

    [SerializeField] GameObject bossArena; 

    [SerializeField] Material invincibleMaterial;
    [SerializeField] Material baseMaterial;
 

    private float invincibilityTime = 10f;

    private float invincibilityCooldown = 10f;  

    // Start is called before the first frame update
    void Start()
    {
        HP = 5;
        bossName = "The Brain";
        attackCooldown = 4f;
        defeated = false;
        stateMachine = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    void OnEnable() {
        HP = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibilityTime > 0) {
            invincibilityTime -= Time.deltaTime;
            // change tag to invincible
            gameObject.tag = "Invincible";
            // change material to invincible
            gameObject.GetComponent<Renderer>().material = invincibleMaterial;
        }
        else {
            gameObject.tag = "Boss";
            invincibilityCooldown -= Time.deltaTime;
            gameObject.GetComponent<Renderer>().material = baseMaterial;
        }
        if (invincibilityCooldown <= 0) {
            invincibilityTime = 10f;
            invincibilityCooldown = 10f;
        }
        if (attackCooldown <= 0) {
            Attack();
            attackCooldown = 2f;
        } else {
            attackCooldown -= Time.deltaTime;
        }
    }

    public override void Attack() {
        // there will be more later.
        int attackChoice = UnityEngine.Random.Range(1, 3); 
        if (attackChoice == 1) {
            stateMachine.SetTrigger("Attack1");
        } 
        else if (attackChoice == 2) {
            stateMachine.SetTrigger("Attack2");
        }
    }
}
