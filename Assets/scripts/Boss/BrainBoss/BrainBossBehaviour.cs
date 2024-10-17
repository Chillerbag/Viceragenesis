using System;
using JetBrains.Annotations;
using UnityEngine;

public class BrainBossBehaviour : BossBehaviour
{
    private Animator stateMachine;

    [SerializeField] GameObject bossArena; 

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
        if (attackCooldown <= 0) {
            Attack();
            attackCooldown = 2f;
        } else {
            attackCooldown -= Time.deltaTime;
        }
    }

    public override void Attack() {
        // there will be more later.
        int attackChoice = 1; 
        if (attackChoice == 1) {
            stateMachine.SetTrigger("Attack1");
        } 
    }
}
