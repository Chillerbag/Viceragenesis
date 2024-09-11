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

    void Update() {
        OnHealthLoss();
    }

    private void OnHealthLoss()
    {
        string NodeToDestroy = "Node." + currentHealth;
        Destroy(GameObject.Find(NodeToDestroy));

        if (currentHealth == 2)
        {
            Destroy(GameObject.Find("Cube.018"));
            Destroy(GameObject.Find("Cube.017"));
            Destroy(GameObject.Find("Cube.016"));
            Destroy(GameObject.Find("Cube.015"));
            Destroy(GameObject.Find("Cube.014"));
            Destroy(GameObject.Find("Cube.013"));
        }

        if (currentHealth == 1)
        {
            Destroy(GameObject.Find("Cube.012"));
            Destroy(GameObject.Find("Cube.011"));
            Destroy(GameObject.Find("Cube.010"));
            Destroy(GameObject.Find("Cube.009"));
            Destroy(GameObject.Find("Cube.008"));
            Destroy(GameObject.Find("Cube.007"));
        }
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