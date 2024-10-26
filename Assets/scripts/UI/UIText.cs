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
        screenText.text = "Health= " + player.GetComponent<PlayerHealth>().currentHealth.ToString();
    }

}
