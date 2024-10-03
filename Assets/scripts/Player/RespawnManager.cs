using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public Vector3 currentRespawnPoint; // current respawn point 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Respawn") {
            Debug.Log("Saving progress...");
            currentRespawnPoint = other.gameObject.GetComponent<Transform>().position;   
            PlayerPrefs.SetFloat("RespawnX", currentRespawnPoint.x);
            PlayerPrefs.SetFloat("RespawnY", currentRespawnPoint.y);
            PlayerPrefs.SetFloat("RespawnZ", currentRespawnPoint.z);
            PlayerPrefs.SetInt("RespawnScene", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
        }
    }
}
