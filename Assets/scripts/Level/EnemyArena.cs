using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class EnemyArena : MonoBehaviour
{

    public bool ArenaActive = false;

    [SerializeField] private TextMeshProUGUI EnemyCountText; 
    // Start is called before the first frame update

    [SerializeField] private Collider[] arenaBoundaries;

    public GameObject[] Enemies;

    public int enemyCount = 8;
    void Start()
    {   
        EnemyCountText.gameObject.SetActive(false);
        SetArenaBoundariesActive(false);
        for (int i = 0; i < Enemies.Length; i++) {
                Enemies[i].SetActive(false);
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemies.Length == 0) {
            ArenaActive = false;
            EnemyCountText.gameObject.SetActive(false);
            SetArenaBoundariesActive(false);
        }

        if (ArenaActive) {
            EnemyCountText.gameObject.SetActive(true);
            EnemyCountText.text = "Enemies Left: " + enemyCount.ToString();

            // check if an enemmy has been destroyed and remove
            for (int i = 0; i < Enemies.Length; i++) {
                if (Enemies[i] == null) {
                    enemyCount -= 1;
                    removeEnemy(Enemies[i]);
                }
            }

        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            ArenaActive = true;
            EnemyCountText.gameObject.SetActive(true);
            SetArenaBoundariesActive(true);
            for (int i = 0; i < Enemies.Length; i++) {
                Enemies[i].SetActive(true);
            }
            
        }
    }

    private void SetArenaBoundariesActive(bool active)
    {
        foreach (Collider boundary in arenaBoundaries)
        {
            boundary.enabled = active;
        }
    }

    public void removeEnemy(GameObject enemy)
    {
        List<GameObject> enemyList = new List<GameObject>(Enemies);
        enemyList.Remove(enemy);
        Enemies = enemyList.ToArray();
    }
}
