using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserMove : MonoBehaviour
{
    private bool moveRight;

    void OnEnable()
    {
        // Randomly choose direction
        moveRight = Random.value > 0.5f;

        if (moveRight)
        {
            transform.position = new Vector3(-169.9f, 55, -135.6f);
            transform.rotation = Quaternion.Euler(0, 45, 90);
        }
        else
        {
            transform.position = new Vector3(3.5f, 55, -135.6f);
            transform.rotation = Quaternion.Euler(0, 135, 90);
        }

        StartCoroutine(MoveLaser());
    }

    private IEnumerator MoveLaser()
    {
        float targetAngle = moveRight ? 135f : 45f;
        float currentAngle = transform.rotation.eulerAngles.y;
        float moveDistance = 2f;

        while (moveRight ? (currentAngle < targetAngle) : (currentAngle > targetAngle))
        {
            float newAngle = moveRight ? currentAngle + 1 : currentAngle - 1;
            transform.rotation = Quaternion.Euler(0, newAngle, 90);
            
            float xMove = moveRight ? moveDistance : -moveDistance;
            transform.position = new Vector3(transform.position.x + xMove, transform.position.y, transform.position.z);

            currentAngle = newAngle;
            yield return new WaitForSeconds(0.03f);
        }

        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }
    }
}