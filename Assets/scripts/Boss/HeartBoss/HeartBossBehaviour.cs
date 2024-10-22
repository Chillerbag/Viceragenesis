using System;
using Unity.VisualScripting;
using UnityEngine;

public class HeartBossBehaviour : BossBehaviour
{
    private Animator stateMachine;
    [SerializeField] GameObject bossArena; 
    public int MaxHealth;
    public HeartBossMainBranch mainBranch;
    public GameObject attack;
    public Material material; 

    // Start is called before the first frame update
    void Awake()
    {
        bossName = "The Heart";
        attackCooldown = 4f;
        stateMachine = GetComponent<Animator>();
        mainBranch = GetComponentInChildren<HeartBossMainBranch>();
    }
    void Start(){
        HP = MaxHealth; 

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
    public override void Damage(int dmg) {
        if (HP >0){
            HP-=dmg;
            Debug.Log(HP);

        } else if (HP<=0) {
            defeated = true;
            Debug.Log("deactive");

            if (gameObject.activeSelf){
                gameObject.SetActive(false);

            }
        }
        if (HP == 5) {
            // change state to enraged
            Debug.Log("Enraged");
            stateMachine.SetTrigger("Enraged");
            mainBranch.Enrage();
            attack.GetComponent<AoeAttack>().material = material;
            
        } 
    }
    // Update is called once per frame
    void Update()
    {
        if (attackCooldown <= 0) {
            Attack();
            attackCooldown = 4f;
        } else {
            attackCooldown -= Time.deltaTime;
        }
    }

        public override void EnrageBoss(){} 

}
