using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbstractEnemy;

public class BasicEnemy : AbstractEnemy.AbstractEnemy
{
    public Transform player;
    public Transform firePoint;

    public void Start()
    {
        shootingInterval = 3f;
        shootingTimer = shootingInterval;
    }



    protected override void Update()
    {
        base.Update();
        FacePlayer();
    }

    void FacePlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    protected override void Attack()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - firePoint.position).normalized;
            direction.y = 0;
            ShootBullet(direction, firePoint.position);
        }
    }
}