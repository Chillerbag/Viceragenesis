using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroPracticeManager : MonoBehaviour
{
    public void loadStartLevel()
    {
        print("here!");
        SceneManager.LoadScene("Level1");
    }


}
