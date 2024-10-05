using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class risingAcid : MonoBehaviour
{
    public bool isRising = false;
    public Transform player;
    // Start is called before the first frame update

    [SerializeField] private TextMeshProUGUI LevelText; 
    void Start()
    {
        LevelText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y > transform.position.y + 30 && !isRising) {
            // wait 5 seconds before acid starts rising
            StartCoroutine(Wait());
            isRising = true;
        }
        if(isRising) {
            transform.position += new Vector3(0, 0.015f, 0);

        }
        if (transform.position.y >= 680) {
            isRising = false;
        }
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(3);
        }
    }

    IEnumerator Wait() {
        LevelText.text = "Acid is rising! Run!";
        LevelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        LevelText.gameObject.SetActive(false);
    }
}
