using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbstractEnemy;
using Unity.VisualScripting;
public class RotatingLaser : MonoBehaviour
{
    public GameObject[] lasers;
    public float rotationSpeed = 25f;

    void Start(){
        //StartCoroutine(Laser());
    }

    protected void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            foreach(GameObject laser in lasers){
                laser.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime,Space.Self);
            }    
        }
/*
    IEnumerator 
    Laser(){
        while(gameObject.activeSelf){
            foreach(GameObject laser in lasers){
                laser.Enabled = true ;
            }
            yield return new WaitForSeconds(1);

        }
    }
    */
}