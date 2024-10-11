using UnityEngine;

public class BulletVaryingSpeed : MonoBehaviour
{
    public float minSpeed = 5f;  // Minimum speed of the bullet
    public float maxSpeed = 20f; // Maximum speed of the bullet
    private Rigidbody rb;        // Reference to the Rigidbody component

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle collision (optional)
        Debug.Log("Bullet hit: " + other.name);
        Destroy(gameObject); // Destroy the bullet on collision
    }

    private void OnBecameInvisible()
    {
        // Optionally destroy the bullet if it goes off-screen
        Destroy(gameObject);
    }
}
