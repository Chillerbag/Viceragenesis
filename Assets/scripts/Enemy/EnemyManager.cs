using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public Transform player;
    public float activationRadius = 20f;
    public float checkInterval = 0.5f;

    private List<Enemy> enemies = new List<Enemy>();
    private float nextCheckTime;

    [System.Serializable]
    public class Enemy
    {
        public GameObject enemyObject;
        public float distanceToPlayer;
    }

    void Start()
    {
        // Find all enemy objects in the scene and add them to the list
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in enemyObjects)
        {
            enemies.Add(new Enemy { enemyObject = obj, distanceToPlayer = 0f });
            obj.SetActive(false); // Start with all enemies deactivated
        }
    }

    void Update()
    {
        if (Time.time >= nextCheckTime)
        {
            CheckEnemyDistances();
            nextCheckTime = Time.time + checkInterval;
        }
    }

    void CheckEnemyDistances()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.distanceToPlayer = Vector3.Distance(enemy.enemyObject.transform.position, player.position);

            if (enemy.distanceToPlayer <= activationRadius && !enemy.enemyObject.activeSelf && enemy.enemyObject.GetComponent<EnemyHealthManager>().HP > 0)
            {
                enemy.enemyObject.SetActive(true);
            }
            else if (enemy.distanceToPlayer > activationRadius && enemy.enemyObject.activeSelf && enemy.enemyObject.GetComponent<EnemyHealthManager>().HP <= 0)
            {
                enemy.enemyObject.SetActive(false);
            }
        }
    }
}