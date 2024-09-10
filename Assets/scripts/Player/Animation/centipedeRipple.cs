using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centipedeRipple : MonoBehaviour
{
    private float cycle; // This variable increases with time and allows the sine to produce numbers between -1 and 1.
    public float waveSpeed = 1f; // Higher make the wave faster
    public float bonusHeight = 3f; // Set higher if you want more wave intensit


    // Update is called once per frame
    void LateUpdate()
    {
        cycle += Time.deltaTime * waveSpeed;

        transform.position += (Vector3.up * bonusHeight) * Mathf.Sin(cycle);
        transform.position += (Vector3.right * bonusHeight) * Mathf.Cos(cycle);
        transform.position += (Vector3.forward * bonusHeight) * Mathf.Cos(cycle);
    }
}
