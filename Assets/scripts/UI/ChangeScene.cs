using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void LoadProgress()
    {
        try
        {
            int sceneIndex = PlayerPrefs.GetInt("RespawnScene");
            SceneManager.LoadScene(sceneIndex);
            Debug.Log("load spawnx"+PlayerPrefs.GetFloat("RespawnX"));
            Debug.Log("load spawny"+PlayerPrefs.GetFloat("RespawnY"));
            Debug.Log("load spawnz"+PlayerPrefs.GetFloat("RespawnZ"));
        }
        catch
        {

            
            SceneManager.LoadScene(4);
        }
    }

    public void newGame()
    {
        // reset player data
        PlayerPrefs.DeleteAll();

        // load the first level 
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
