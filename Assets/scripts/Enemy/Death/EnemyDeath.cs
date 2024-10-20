 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public virtual void Death(){
        gameObject.SetActive(false);
    }
}
