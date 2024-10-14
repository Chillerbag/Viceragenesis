using UnityEngine;
using System.Collections;

public class PlayerAttacking : MonoBehaviour
{
    private PlayerState playerState;
    private CharacterController controller;
    private Animator rigAnimator;

    public GameObject hitParticles;

    public PlayerHealth playerHealth;


    void Start()
    {
        playerState = GetComponent<PlayerState>();
        controller = GetComponent<CharacterController>();
        rigAnimator = GetComponentInChildren<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.tag);

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "ArenaEnemy")
        {
            Debug.Log("Collision with enemy or boss detected.");

            if (playerState.GetState() == "Attacking")
            {
                Debug.Log("Player is in Attacking state.");

                Vector3 bounceDirection = -collision.GetContact(0).normal; // Opposite of collision normal
                Vector3 hitPoint = collision.GetContact(0).point;
                Quaternion hitRotation = Quaternion.LookRotation(hitPoint);
                Instantiate(hitParticles, hitPoint, hitRotation);
                Debug.Log("Calculated bounce direction: " + bounceDirection);

                if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "ArenaEnemy")
                {
                    BounceBack(bounceDirection.normalized, collision.gameObject.tag);
                    collision.gameObject.GetComponent<EnemyHealthManager>().DamageToEnemy(1);
                }
                else if (collision.gameObject.tag == "Boss")
                {
                    BounceBack(bounceDirection.normalized, collision.gameObject.tag);
                    collision.gameObject.GetComponent<TemplateBossBehaviour>().DamageToBoss(1);
                }
            }
            else
            {
                Debug.Log("Player is not in Attacking state.");
            }
        }
    }

    // this is out of scope for this script. Fix later.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Acid")
        {
            playerHealth.TakeDamage(1);
            // bounce into the air
            StartCoroutine(SmoothBounceUp());
            Debug.Log("Player hit acid and took damage.");
        }
        else if (other.gameObject.tag == "Lightning") {
            playerHealth.TakeDamage(1);
        }

    }

    private void BounceBack(Vector3 bounceBack, string enemyTag)
    {
        float bounceDistance = 0.8f; // Adjust this value for a more satisfying bounce
        Vector3 bounceVector = -1 * bounceBack * bounceDistance * 15;
        if (enemyTag == "Boss")
        {
            bounceDistance = 1.5f; // Adjust this value for a more satisfying bounce
            bounceVector = -1 * bounceBack * bounceDistance * 20;
        }
        Debug.Log("Applying bounce back with vector: " + bounceVector);
        rigAnimator.SetTrigger("attackBounce");
        StartCoroutine(ApplyBounceBack(bounceVector));
        rigAnimator.SetTrigger("returnToIdle");
    }

    private IEnumerator SmoothBounceUp()
    {
        float bounceHeight = 25f; // Total height to bounce
        float duration = 0.5f; // Duration of the bounce
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.up * bounceHeight;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            controller.Move(Vector3.Lerp(initialPosition, targetPosition, t) - transform.position);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ApplyBounceBack(Vector3 bounceVector)
    {
        float duration = 0.7f; // Duration of the bounce-back effect
        float elapsedTime = 0f;
        float arcHeight = 2.5f; // Adjust this value for a higher or lower arc

        // Calculate the initial and final positions
        Vector3 initialPosition = transform.position;
        Vector3 finalPosition = initialPosition + bounceVector;

        // Calculate the arc height

        // Calculate the initial and final rotations
        Quaternion initialRotation = transform.rotation;
        Quaternion finalRotation = Quaternion.LookRotation(-bounceVector);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Interpolate position with an arc
            Vector3 currentPosition = Vector3.Lerp(initialPosition, finalPosition, t);
            currentPosition.y += arcHeight * Mathf.Sin(Mathf.PI * t); // Create the arc

            // Interpolate rotation
            Quaternion currentRotation = Quaternion.Slerp(initialRotation, finalRotation, t);

            // Apply position and rotation
            controller.Move(currentPosition - transform.position);
            transform.rotation = currentRotation;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Bounce back applied.");
    }
}