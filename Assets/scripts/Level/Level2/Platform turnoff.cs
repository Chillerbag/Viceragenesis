using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformTurnOff : MonoBehaviour
{
    private Vector3 startPosition;

    public GameObject[] gameObjects;

    public GameObject[] Entrance;

    private Boolean active =true;

    public Material material;

    void Start()
    {
        active = true; 
        startPosition = transform.position;
    }

    void Update()
    {   
        bool flag = false;
        foreach (GameObject gameObject in gameObjects){
            if (! gameObject.GetComponent<EnemyHealthManager>().isDead){
                
                flag = true;
            }
        }
        if (!flag& active){
            foreach (GameObject gameObject in Entrance){
                gameObject.GetComponent<Renderer>().material = material;
                gameObject.GetComponent<Collider>().isTrigger =false;
            }
            active = false;
        } 
    }
}