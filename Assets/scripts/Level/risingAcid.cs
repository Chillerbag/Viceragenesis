using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class risingAcid : MonoBehaviour
{
    public bool isRising = false;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y > transform.position.y + 30) {
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
        yield return new WaitForSeconds(5);
    }
}
