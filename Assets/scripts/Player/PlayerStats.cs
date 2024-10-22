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
    public GameObject screenFlash;

    private PlayerEffects playerEffects;

    [SerializeField] private GameObject deathEffect;

    [SerializeField] private GameObject[] bodyCubes;

    private Vector3 respawnPoint;

    void Start()
    {
        Debug.Log("Start method called.");
        currentHealth = maxHealth;
        playerEffects = GetComponent<PlayerEffects>();
        // check if we're in a level where we should load progress
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "LeveL2")
        {
            LoadProgress();
        }
        Debug.Log("actual spawn at " +transform.position);
    }

private void bodyCubesHandler() {
    // check health and determine what cubes to show
    switch (currentHealth) {
        case 3: 
            foreach (GameObject cube in bodyCubes) {
                cube.SetActive(true);
            }
            break;
        case 2:
            // first 6 els of bodyCubes should be disabled
            for (int i = 0; i < 5; i++) {
                bodyCubes[i].SetActive(false);
            }
            //remainder should be enabled
            for (int i = 5; i < bodyCubes.Length; i++) {
                bodyCubes[i].SetActive(true);
            }
            break;
        case 1:
            // first 12 els of body cubes should be disabled
            for (int i = 0; i < 10; i++) {
                bodyCubes[i].SetActive(false);
            }
            // remainder should be enabled
            for (int i = 10; i < bodyCubes.Length; i++) {
                bodyCubes[i].SetActive(true);
            }
            break;
    }
}
    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return; // If the player is invulnerable, do nothing

        // handle audio
        playerEffects.PlayDamageSound();

        currentHealth -= damage;
        bodyCubesHandler();

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

    public void invulnerability()
    {
        StartCoroutine(InvulnerabilityCoroutine());
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        playerEffects.PlayHealthSound();

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        bodyCubesHandler();

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
        //Debug.Log("Player Died!");
        StartCoroutine(ChangeToDeathScreen());

    }

    private IEnumerator ChangeToDeathScreen()
    {
        ReduceBitDepth reduceBitDepthComponent = deathEffect.GetComponent<ReduceBitDepth>();
        reduceBitDepthComponent.ReduceScreenBitDepth();
        yield return new WaitForSeconds(reduceBitDepthComponent.reductionDuration + 1);
        reduceBitDepthComponent.ResetScreenBitDepth();
        SceneManager.LoadScene("Game Over");
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