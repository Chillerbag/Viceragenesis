using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    void Start() {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    public void StrikeLightning() {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(StrikeLightningCoroutine());
    }
    public IEnumerator StrikeLightningCoroutine() {
        yield return new WaitForSeconds(2f); 
        // activate the lightning strike effect and wait for 1 second
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
