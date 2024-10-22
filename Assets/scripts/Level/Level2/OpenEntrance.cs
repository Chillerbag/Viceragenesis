using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenEntrance : MonoBehaviour
{
    public float frequency = 1f;
    public float amplitude = 1f;
    private Vector3 startPosition;

    public GameObject[] gameObjects;

    public GameObject[] Entrance;

    private Boolean active =true;

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
                gameObject.transform.Rotate(40f,0f,0f,Space.Self);

            }
            active = false;
        } 
    }
}