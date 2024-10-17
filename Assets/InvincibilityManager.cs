using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityManager : MonoBehaviour
{

    private float invincibilityTime = 10f;

    private float invincibilityCooldown = 10f;  

    [SerializeField] GameObject invincibleShield;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

         if (invincibilityTime > 0) {
            invincibilityTime -= Time.deltaTime;
            // change tag to invincible
            gameObject.tag = "Invincible";
            // activate invincibility shield
            invincibleShield.SetActive(true);
        }
        else {
            gameObject.tag = "Boss";
            invincibleShield.SetActive(false);
        } 
    }

    public void ActivateInvincibility() {
        invincibilityTime = 10f;
    }
}
