using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public GameObject screenFlash;


    [SerializeField] private AudioClip damageSound;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // handle audio
        SoundFXManager.instance.PlaySoundFXClip(damageSound, transform, 1f);

        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (screenFlash != null)
        {
            screenFlash.GetComponent<flashEffect>().Flash();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        SceneManager.LoadScene("GameOverScreen");
    }
}