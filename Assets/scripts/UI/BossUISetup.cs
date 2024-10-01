using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUISetup : MonoBehaviour
{
    [SerializeField] private Slider BossHealth;
    [SerializeField] private TextMeshProUGUI bossText;

    private GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss");
        bossText.text = Boss.GetComponent<StomachBossBehaviour>().bossName;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Boss != null) {
            BossHealth.value = Boss.GetComponent<StomachBossBehaviour>().HP;
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
