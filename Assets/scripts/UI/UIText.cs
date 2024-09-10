using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIText : MonoBehaviour
{
    public TextMeshProUGUI screenText;  
    public GameObject player;   

    void Update()
    {
        // Assuming your Player script has public Health and Status properties
        screenText.text = "Health= " + player.GetComponent<PlayerHealth>().currentHealth.ToString() + " Status= " + player.GetComponent<basicPlayerMovement>().State;
    }

}
