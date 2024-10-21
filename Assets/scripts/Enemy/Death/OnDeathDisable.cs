using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathDisable : EnemyDeath
{
    // Start is called before the first frame update
    public GameObject[] gameObjects;
    // Update is called once per frame
    public override void Death()
    {
        foreach(GameObject gameObjects in gameObjects){
            gameObjects.GetComponent<Bouncing>().active = true;
        }
    }
}
