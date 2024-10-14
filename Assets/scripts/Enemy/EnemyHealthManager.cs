using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int HP;

    public void DamageToEnemy(int dmg) {
        HP-=dmg;
        if (HP<=0) {
            gameObject.SetActive(false);
        }
    }
}
