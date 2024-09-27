using UnityEngine;

public class StomachCystBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        GameObject.FindGameObjectWithTag("Boss").GetComponent<StomachBossBehaviour>().DamageToStomachBoss(1);
    }
}
