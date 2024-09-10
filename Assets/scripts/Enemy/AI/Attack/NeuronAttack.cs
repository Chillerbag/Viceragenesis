using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NeuronAttack : MonoBehaviour, IAttackable
{
    public GameObject EnemySpawn; 
    
    public Vector3 spawnPoint;
    public float enemyGenerateRange = 10f;

    public LayerMask Ground;
    public void Attack()
    {
        // While the spawnPoint is on the ground. 
        enemyGeneratePoint();
        while (!Physics.Raycast(spawnPoint, -transform.up,2f,Ground)){
            enemyGeneratePoint();
        }

        GameObject enemy = Instantiate(EnemySpawn, spawnPoint, transform.rotation);

    }
    private void enemyGeneratePoint()
    {
        float randomZ = UnityEngine.Random.Range(-enemyGenerateRange, enemyGenerateRange);
        float randomX = UnityEngine.Random.Range(-enemyGenerateRange, enemyGenerateRange);

        spawnPoint = new Vector3(transform.position.x+randomX, transform.position.y, transform.position.z + randomZ);
    }   

}

