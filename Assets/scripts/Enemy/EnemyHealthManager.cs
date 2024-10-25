using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int HP = 1;
    private EnemyDeath[] DeathEffects;
    public Boolean isDead = false;
    public virtual void DamageToEnemy(int dmg) {
        HP-=dmg;
        if (HP<=0 && !isDead) {
            isDead = true;
            DeathEffects = GetComponents<EnemyDeath>();
            Debug.Log(DeathEffects == null);
                Debug.Log(DeathEffects.Length == 0);

            if (DeathEffects != null && DeathEffects.Length != 0)
            {   
                foreach(EnemyDeath deathEffect in DeathEffects){
                    deathEffect.Death();
                }
            } else{
                gameObject.SetActive(false);

            }
        }
    }

    public Boolean getIsDead(){
        return isDead;
    }
}
