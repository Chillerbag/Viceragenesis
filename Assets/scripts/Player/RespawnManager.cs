using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;


public class RespawnManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LevelText; 
    public Vector3 currentRespawnPoint; // current respawn point 
    // Start is called before the first frame update
    void Start()
    {
        LevelText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Respawn") {
            StartCoroutine(saveProgressText("Respawn"));
            currentRespawnPoint = other.gameObject.GetComponent<Transform>().position;   
            PlayerPrefs.SetFloat("RespawnX", currentRespawnPoint.x);
            PlayerPrefs.SetFloat("RespawnY", currentRespawnPoint.y);
            PlayerPrefs.SetFloat("RespawnZ", currentRespawnPoint.z);
            PlayerPrefs.SetInt("RespawnScene", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
            Debug.Log("set respawn" + currentRespawnPoint+ "in the scene" + SceneManager.GetActiveScene().buildIndex);
            Debug.Log("spawnx"+PlayerPrefs.GetFloat("RespawnX"));
            Debug.Log("spawny"+PlayerPrefs.GetFloat("RespawnY"));
            Debug.Log("spawnz"+PlayerPrefs.GetFloat("RespawnZ"));

        }
        if (other.gameObject.tag == "SpawnPoint") {
            StartCoroutine(saveProgressText("SpawnPoint"));
            currentRespawnPoint = other.gameObject.GetComponent<Transform>().position;   
            PlayerPrefs.SetFloat("RespawnX", currentRespawnPoint.x);
            PlayerPrefs.SetFloat("RespawnY", currentRespawnPoint.y);
            PlayerPrefs.SetFloat("RespawnZ", currentRespawnPoint.z);
            PlayerPrefs.SetInt("RespawnScene", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();

            Debug.Log("set respawn" + currentRespawnPoint+ "in the scene" + SceneManager.GetActiveScene().buildIndex);
            Debug.Log("spawnx"+PlayerPrefs.GetFloat("RespawnX"));
            Debug.Log("spawny"+PlayerPrefs.GetFloat("RespawnY"));
            Debug.Log("spawnz"+PlayerPrefs.GetFloat("RespawnZ"));
        }
    }

    IEnumerator saveProgressText(String type) {
        // if its the spawn point, display some different text
        if (type == "SpawnPoint") {
            // get the level
            if (SceneManager.GetActiveScene().buildIndex == 4) {
                LevelText.text = "Level 1: The Stomach";
            } 
            if (SceneManager.GetActiveScene().buildIndex == 5) {
                LevelText.text = "Level 2: The Heart";
            } 
            if (SceneManager.GetActiveScene().buildIndex == 6) {
                LevelText.text = "Final Level: The Brain";
            } 
        } else {
            LevelText.text = "Respawn point saved!";
        }
        LevelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        LevelText.gameObject.SetActive(false);
    }
}
