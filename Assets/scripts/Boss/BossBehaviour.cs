using System;
using JetBrains.Annotations;
using UnityEngine;

public abstract class BossBehaviour : MonoBehaviour
{
    public int HP {get; set;}

    public string bossName {get; set;}

    public float attackCooldown {get; set;}

    public bool defeated = false;


    void OnEnable() {
        HP = 3;
    }

    public virtual void Attack() {

    }
    
    public virtual void Damage(int dmg) {
        HP-=dmg;

        if (HP<=0) {
            defeated = true;
            gameObject.SetActive(false);
        }
    }

    public virtual void EnrageBoss() 
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
