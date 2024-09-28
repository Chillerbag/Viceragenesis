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
        int sceneIndex = PlayerPrefs.GetInt("RespawnScene");
        SceneManager.LoadScene(sceneIndex);
    }

    public void newGame()
    {
        // reset player data
        PlayerPrefs.DeleteAll();

        // load the first level 
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
