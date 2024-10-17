using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserMove : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.position = new Vector3(-169.9f, 55, -135.6f);
        transform.rotation = Quaternion.Euler(0, 45, 90);
        StartCoroutine(MoveLaser());
    }

    private IEnumerator MoveLaser() {
        // gradually rotate the laser to 135 degrees on y
        while (transform.rotation.eulerAngles.y < 135) {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 1, 90);
            yield return new WaitForSeconds(0.05f);
            transform.position = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
        }
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Try to get the PlayerHealth component from the collided object
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // If the component exists, apply damage
            playerHealth.TakeDamage(1);
        }
    }
}   
