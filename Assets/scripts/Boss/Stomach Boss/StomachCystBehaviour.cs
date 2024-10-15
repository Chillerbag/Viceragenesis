using UnityEngine;

public class StomachCystBehaviour : MonoBehaviour
{
    public void OnDisable() {
        if (gameObject.GetComponent<EnemyHealthManager>().HP <= 0) {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBehaviour>().Damage(1);
        }
    }
}
