using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public float invulnerabilityDuration = 2f; // Duration of invulnerability in seconds
    private bool isInvulnerable = false;

    private int currentLevel = 1;

    public GameObject screenFlash;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private GameObject deathEffect;

    private Vector3 respawnPoint;

    void Start()
    {
        Debug.Log("Start method called.");
        currentHealth = maxHealth;
        LoadProgress();
    }

    void Update()
    {
        OnHealthLoss();
        OnHealthGain();
    }

    private void OnHealthLoss()
{
    string NodeToDestroy = "Node." + currentHealth;
    GameObject node = GameObject.Find(NodeToDestroy);
    if (node != null)
    {
        node.SetActive(false);
    }

    if (currentHealth == 2)
    {
        SetActiveStateForCubes(false, "Cube.018", "Cube.017", "Cube.016", "Cube.015", "Cube.014", "Cube.013");
    }

    if (currentHealth == 1)
    {
        SetActiveStateForCubes(false, "Cube.012", "Cube.011", "Cube.010", "Cube.009", "Cube.008", "Cube.007");
    }
}

private void OnHealthGain()
{
    string NodeToRestore = "Node." + currentHealth;
    GameObject node = GameObject.Find(NodeToRestore);
    if (node != null)
    {
        node.SetActive(true);
    }

    if (currentHealth == 3)
    {
        SetActiveStateForCubes(true, "Cube.018", "Cube.017", "Cube.016", "Cube.015", "Cube.014", "Cube.013");
    }

    if (currentHealth == 2)
    {
        SetActiveStateForCubes(true, "Cube.012", "Cube.011", "Cube.010", "Cube.009", "Cube.008", "Cube.007");
    }
}

private void SetActiveStateForCubes(bool state, params string[] cubeNames)
{
    foreach (string cubeName in cubeNames)
    {
        GameObject cube = GameObject.Find(cubeName);
        if (cube != null)
        {
            cube.SetActive(state);
        }
    }
}

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return; // If the player is invulnerable, do nothing

        // handle audio
        SoundFXManager.instance.PlaySoundFXClip(damageSound, transform, 1f);

        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (screenFlash != null)
        {
            screenFlash.GetComponent<flashEffect>().FlashHurt();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (screenFlash != null)
        {
            screenFlash.GetComponent<flashEffect>().FlashHeal();
        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    void Die()
    {
        Debug.Log("Player Died!");
        StartCoroutine(ChangeToDeathScreen());

    }

    private IEnumerator ChangeToDeathScreen()
    {
        ReduceBitDepth reduceBitDepthComponent = deathEffect.GetComponent<ReduceBitDepth>();
        reduceBitDepthComponent.ReduceScreenBitDepth();
        yield return new WaitForSeconds(reduceBitDepthComponent.reductionDuration + 1);
        reduceBitDepthComponent.ResetScreenBitDepth();
        SceneManager.LoadScene("GameOverScreen");
    }

    public void LoadProgress()
    {
        Debug.Log("Loading progress...");
        if (PlayerPrefs.HasKey("RespawnX"))
        {
            float x = PlayerPrefs.GetFloat("RespawnX");
            Debug.Log("RespawnX: " + x);
            float y = PlayerPrefs.GetFloat("RespawnY");
            Debug.Log("RespawnY: " + y);
            float z = PlayerPrefs.GetFloat("RespawnZ");
            Debug.Log("RespawnZ: " + z);
            respawnPoint = new Vector3(x, y, z);
            GetComponent<Transform>().position = respawnPoint;
        } else {
            respawnPoint = GameObject.Find("SpawnPoint").transform.position;
            Debug.Log("RespawnPoint: " + respawnPoint);
            GetComponent<Transform>().position = respawnPoint;
        }
    }
}