using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    public void StrikeLightning() {
        StartCoroutine(StrikeLightningCoroutine());
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = false;

    }
    public IEnumerator StrikeLightningCoroutine() {
        // show our model to indicate where the lightning will strike
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(1f); 
        // activate the lightning strike effect and wait for 1 second
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);

    }
}
