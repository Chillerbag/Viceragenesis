using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStoryManager : MonoBehaviour
{
    public void OnStorySwitch()
    {
        SceneManager.LoadScene(2);
    }
}
