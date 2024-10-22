using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathChangeColour : EnemyDeath
{
    // Start is called before the first frame update
    public GameObject[] gameobjects;
    public Material material;
    // Update is called once per frame
    public override void Death()
    {
        foreach(GameObject gameObject in gameobjects){
            Renderer render = gameObject.GetComponent<Renderer>();
        
            render.material = material;
        }
    }
}
