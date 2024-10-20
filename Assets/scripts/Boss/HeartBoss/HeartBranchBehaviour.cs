using UnityEngine;

public class HeartBranchBehaviour : EnemyDeath
{   
    public GameObject boss;

        // Start is called before the first frame update
    public GameObject[] gameobjects;
    public Material material;
    // Update is called once per frame

    // Start is called before the first frame update
    void Start()
    {   
        
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    public override void Death()
    {
        boss.GetComponent<BossBehaviour>().Damage(1);

        foreach(GameObject gameObject in gameobjects){
            Renderer render = gameObject.GetComponent<Renderer>();
        
            render.material = material;
        }
        GetComponentInChildren<AoeAttack>().active = false;
    }


}
 
