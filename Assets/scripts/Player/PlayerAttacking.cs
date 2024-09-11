using UnityEngine;
using System.Collections;

public class PlayerAttacking : MonoBehaviour
{
    private Vector3 lastDirection;

    //private PlayerMovement playerMovement;

    private PlayerState playerState;

    void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            if (playerState.GetState() == "Attacking")
            {
                Vector3 reflection = Vector3.Reflect(lastDirection, collision.contacts[0].normal);
                if (collision.gameObject.tag == "Enemy")
                {
                    collision.gameObject.GetComponent<EnemyHealthManager>().DamageToEnemy(1);
                }
                else if (collision.gameObject.tag == "Boss")
                {
                    collision.gameObject.GetComponent<TemplateBossBehaviour>().DamageToBoss(1);
                }
                StartCoroutine(BounceBack(reflection.normalized));
            }
        }
    }

    private IEnumerator BounceBack(Vector3 bounceBack)
    {
        float bounceDuration = 0.5f;
        float elapsedTime = 0f;
        Rigidbody rb = GetComponent<Rigidbody>();

        while (elapsedTime < bounceDuration)
        {
            float t = elapsedTime / bounceDuration;
            rb.AddForce(bounceBack * 20 * (1 - t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}