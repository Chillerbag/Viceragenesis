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

    [SerializeField] private GameObject music;

    public int enemyCount = 6;
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
            music.GetComponent<MusicHandler>().changeMusic(0);
            music.GetComponent<MusicHandler>().changeVolume(0.6f);
            EnemyCountText.gameObject.SetActive(false);
            SetArenaBoundariesActive(false);
            gameObject.SetActive(false);
        }

        if (ArenaActive) {
            EnemyCountText.gameObject.SetActive(true);
            EnemyCountText.text = "Enemies Left: " + enemyCount.ToString();

            // check if an enemmy has been destroyed and remove
            for (int i = 0; i < Enemies.Length; i++) {
                if (Enemies[i].GetComponent<EnemyHealthManager>().HP <= 0) {
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
            music.GetComponent<MusicHandler>().changeMusic(1);
            // lower volume
            music.GetComponent<MusicHandler>().changeVolume(0.3f);
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
