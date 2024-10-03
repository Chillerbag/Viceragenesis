using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroPracticeManager : MonoBehaviour
{
    public void OnStart()
    {
        SceneManager.LoadScene("Level1");
    }
}
