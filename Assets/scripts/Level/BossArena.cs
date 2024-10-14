using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // if boss dies take down the collider boundaries
        if (Boss == null ) {
            SetArenaBoundariesActive(false);
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
}
