using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUISetup : MonoBehaviour
{
    [SerializeField] private Slider BossHealth;
    [SerializeField] private TextMeshProUGUI bossText;

    public GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        bossText.text = Boss.GetComponent<StomachBossBehaviour>().bossName;
        
    }

    // Update is called once per frame
    void Update()
    {
            BossHealth.value = Boss.GetComponent<StomachBossBehaviour>().HP;

            if (Boss.GetComponent<StomachBossBehaviour>().HP <= 0) {
                BossHealth.gameObject.SetActive(false);
            }
            else {
                BossHealth.gameObject.SetActive(true);
            }
    }
}
