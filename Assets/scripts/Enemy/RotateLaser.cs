using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbstractEnemy;
using Unity.VisualScripting;
public class RotatingLaser : MonoBehaviour
{
    public float rotationSpeed = 25f;

    void Start(){
    }

    protected void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
}