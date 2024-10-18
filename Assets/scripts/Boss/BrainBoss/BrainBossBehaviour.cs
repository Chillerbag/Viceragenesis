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
        HP = 6;
        bossName = "The Brain";
        attackCooldown = 1f;
        defeated = false;
        stateMachine = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    void OnEnable() {
        HP = 6;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (attackCooldown <= 0) {
            Attack();
            attackCooldown = 1f;
        } else {
            attackCooldown -= Time.deltaTime;
        }
    }

    public override void Attack() {
        int attackChoice = UnityEngine.Random.Range(1, 6); 
        stateMachine.SetTrigger("Attack" + attackChoice);
    }
}
