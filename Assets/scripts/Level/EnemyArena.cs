using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class EnemyArena : MonoBehaviour
{

    public bool ArenaActive = false;

    [SerializeField] private TextMeshProUGUI EnemyCountText; 
    // Start is called before the first frame update

    [SerializeField] private Collider[] arenaBoundaries;

    public GameObject[] Enemies;

    public int enemyCount = 3;
    void Start()
    {   
        SetArenaBoundariesActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemies == null) {
            ArenaActive = false;
            EnemyCountText.gameObject.SetActive(false);
            EnemyCountText.text = "Enemies Left: " + enemyCount.ToString();
            SetArenaBoundariesActive(false);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            ArenaActive = true;
            EnemyCountText.gameObject.SetActive(true);
            SetArenaBoundariesActive(true);
            
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
