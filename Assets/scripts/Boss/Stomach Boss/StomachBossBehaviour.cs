using System;
using JetBrains.Annotations;
using UnityEngine;

public class StomachBossBehaviour : BossBehaviour
{
    private Animator stateMachine;

    [SerializeField] GameObject bossArena; 

    // Start is called before the first frame update
    void Start()
    {
        HP = 3;
        bossName = "The Stomach";
        attackCooldown = 4f;
        defeated = false;
        stateMachine = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    void OnEnable() {
        HP = 3;
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


    public override void Damage(int dmg) {
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

    public override void Attack() {
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
}
