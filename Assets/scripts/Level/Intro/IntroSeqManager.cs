using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSeqManager : MonoBehaviour
{
    public void OnSeqSwitch()
    {
        SceneManager.LoadScene("Intro Practice", LoadSceneMode.Single);
    }
}
