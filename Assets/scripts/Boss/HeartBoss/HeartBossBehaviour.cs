using System;
using UnityEngine;

public class HeartBossBehaviour : BossBehaviour
{
    private Animator stateMachine;

    [SerializeField] GameObject bossArena; 

    [SerializeField] private GameObject Boss;

    // Start is called before the first frame update
    void Start()
    {
        HP = 3; 
        bossName = "The Heart";
        attackCooldown = 4f;
        stateMachine = GetComponent<Animator>();

        
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
}
