using UnityEngine;

public class StomachCystBehaviour : MonoBehaviour
{
    void OnDisable() {
        GameObject.FindGameObjectWithTag("Boss").GetComponent<StomachBossBehaviour>().DamageToStomachBoss(1);
    }
}
