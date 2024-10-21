using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbstractEnemy;
using Unity.VisualScripting.Antlr3.Runtime;
public class RotatingEnemy : AbstractEnemy.AbstractEnemy
{
    public Transform[] firePoints;
    public float rotationSpeed = 25f;

    public void Start()
    {
        shootingInterval = 3f;
        shootingTimer = shootingInterval;
    }

    protected override void Update()
    {
        base.Update();
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    protected override void Attack()
    {
        foreach (Transform firePoint in firePoints)
        {
            if (firePoint != null)
            {
                ShootBullet(firePoint.forward, firePoint.position);
            }
        }
    }
}