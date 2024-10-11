using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int HP = 1;
    private EnemyDeath DeathEffect;

    public void DamageToEnemy(int dmg) {
        HP-=dmg;
        if (HP<=0) {
            DeathEffect = GetComponent<EnemyDeath>();
            if (DeathEffect != null)
            {   
                DeathEffect.Death();
            } else{
                Destroy(gameObject);

            }
        }
    }
}
