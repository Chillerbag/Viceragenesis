using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int HP = 1;
    private EnemyDeath DeathEffect;
    private Boolean isDead = false;
    public void DamageToEnemy(int dmg) {
        HP-=dmg;
        if (HP<=0) {
            isDead = true;
            DeathEffect = GetComponent<EnemyDeath>();
            if (DeathEffect != null)
            {   
                DeathEffect.Death();
            } else{
                gameObject.SetActive(false);

            }
        }
    }

    public Boolean getIsDead(){
        return isDead;
    }
}
