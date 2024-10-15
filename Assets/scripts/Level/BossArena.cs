using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossArena : MonoBehaviour
{
    [SerializeField] private Slider BossHealth; 
    // Start is called before the first frame update

    [SerializeField] private Collider[] arenaBoundaries;

    [SerializeField] private GameObject Boss;
    void Start()
    {
        SetArenaBoundariesActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Boss.GetComponent<StomachBossBehaviour>().defeated == true) {
            SetArenaBoundariesActive(false);
            // move to next scene 
            StartCoroutine(BossDefeated());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Boss.SetActive(true);
            BossHealth.gameObject.SetActive(true);
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

    public IEnumerator BossDefeated() {
        yield return new WaitForSeconds(2);
    }
}
