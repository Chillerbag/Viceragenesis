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

    void OnDisable() {
        if (boss != null){
            boss.GetComponent<BossBehaviour>().Damage(1);

        }
    }
}
