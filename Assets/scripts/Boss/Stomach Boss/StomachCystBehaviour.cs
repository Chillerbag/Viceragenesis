using UnityEngine;

public class StomachCystBehaviour : MonoBehaviour
{
    public void OnDisable() {
        if (gameObject.GetComponent<EnemyHealthManager>().HP <= 0) {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<StomachBossBehaviour>().DamageToStomachBoss(1);
        }

    }
}
