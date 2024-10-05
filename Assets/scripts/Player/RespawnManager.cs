using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


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
            StartCoroutine(saveProgressText());
            currentRespawnPoint = other.gameObject.GetComponent<Transform>().position;   
            PlayerPrefs.SetFloat("RespawnX", currentRespawnPoint.x);
            PlayerPrefs.SetFloat("RespawnY", currentRespawnPoint.y);
            PlayerPrefs.SetFloat("RespawnZ", currentRespawnPoint.z);
            PlayerPrefs.SetInt("RespawnScene", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
        }
    }

    IEnumerator saveProgressText() {
        LevelText.text = "Progress saved!";
        LevelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        LevelText.gameObject.SetActive(false);
    }
}
