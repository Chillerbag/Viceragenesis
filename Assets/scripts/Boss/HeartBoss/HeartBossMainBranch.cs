using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeartBossMainBranch : EnemyHealthManager
{
    public Boolean isInvincible = true;
    public GameObject boss;
    public Material material;

    public void Enrage()
    {

        GetComponent<Renderer>().material = material;
        isInvincible = false;
    }

    public override void DamageToEnemy(int dmg) {
        if (!isInvincible){
            boss.GetComponent<BossBehaviour>().Damage(1);
        }
    }

}