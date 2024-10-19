using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroPracticeManager : MonoBehaviour
{
    public void loadStartLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void loadPracticeLevel()
    {
        SceneManager.LoadScene("Intro Practice");
    }


}
