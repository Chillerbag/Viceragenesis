using UnityEngine;

public class HeartBranchBehaviour : MonoBehaviour
{   
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        if (boss != null)
        {
            // Cache the HeartBossBehaviour component
            HeartBossBehaviour heartBoss = boss.GetComponent<HeartBossBehaviour>();
            
            if (heartBoss != null)
            {
                // Call the method
                Debug.Log("damage the boss");
                heartBoss.DamageToStomachBoss(1);
            }
            else
            {
                Debug.LogWarning("HeartBossBehaviour component not found on Boss.");
            }
        }
        else
        {
            Debug.LogWarning("Boss GameObject not found.");
        }
    }
}
