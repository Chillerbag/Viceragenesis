using System;
using UnityEngine;

public class StomachBossBehaviour : MonoBehaviour
{
    private Animator stateMachine;
    public int HP;

    public string bossName = "The Stomach";

    private float attackCooldown = 4f;

    [SerializeField] GameObject bossArena; 

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossArena.GetComponent<BossArena>().bossActive) {
            if (attackCooldown <= 0) {
                Attack();
                attackCooldown = 2f;
            } else {
                attackCooldown -= Time.deltaTime;
            }
        }
    }


    private void Attack() {
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
    
    public void DamageToStomachBoss(int dmg) {
        HP-=dmg;

        if (HP == 1) {
            // change state to enraged
            stateMachine.SetTrigger("Enraged");
        }

        if (HP<=0) {
            // we should probably trigger a Destroy state here. 
            Destroy(gameObject);
        }

    }

    public void EnrageBoss() 
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