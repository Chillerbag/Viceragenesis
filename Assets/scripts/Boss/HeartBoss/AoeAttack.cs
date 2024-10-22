using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class AoeAttack : MonoBehaviour
{
    public float maxSize = 2f;

    public float currentSize;
    public float increaseSpeed = 1f;
    public Vector3 startScale;
    public GameObject branch;
    public Material GlowMaterial;
    public bool isAttacking = false;
    public bool active = true;
    public Material material; 
    void Start() {
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        startScale = transform.localScale;
        currentSize = 1f;
        isAttacking = false;
        material = branch.GetComponent<Renderer>().material;
    }

    public void Update(){
        if((! isAttacking) & active ){
            Aoe();
        }

    }
    public void Aoe() {
        StartCoroutine(startAttack());
    }

    public IEnumerator startAttack() {
        isAttacking = true;
        branch.GetComponent<Renderer>().material = GlowMaterial;

        while (currentSize < maxSize){
            currentSize += increaseSpeed*Time.deltaTime/currentSize ;
            Vector3 newScale = startScale;
            newScale.x = startScale.x * currentSize;
            newScale.z = startScale.z * currentSize;
            transform.localScale =newScale;

        yield return null;
        }
        yield return new WaitForSeconds(0.75f);
        branch.GetComponent<Renderer>().material = material;

        while (currentSize > 1){
            currentSize -= increaseSpeed*Time.deltaTime/currentSize ;
            Vector3 newScale = startScale;
            newScale.x = startScale.x * currentSize;
            newScale.z = startScale.z * currentSize;
            transform.localScale =newScale;

        yield return null;
        }
        currentSize = 1f;
        transform.localScale = startScale;
        yield return new WaitForSeconds(0.75f);
        isAttacking = false;

    }

    public void OnTriggerEnter(Collider collision) {

        if (collision.gameObject.tag == "Player") {
            // Try to get the PlayerHealth component from the collided object
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // If the component exists, apply damage
                playerHealth.TakeDamage(1);
            }
        }
    }
}
