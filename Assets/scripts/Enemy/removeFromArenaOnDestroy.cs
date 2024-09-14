using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeFromArenaOnDestroy : MonoBehaviour
{
    void OnDestroy() {
        GameObject arena = GameObject.Find("EnemyArena");
        if (arena != null) {
            arena.GetComponent<EnemyArena>().removeEnemy(gameObject);
        }
    }

}
