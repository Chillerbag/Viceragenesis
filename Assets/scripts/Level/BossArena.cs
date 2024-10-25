using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossArena : MonoBehaviour
{
    [SerializeField] private GameObject BossHealth;
    // Start is called before the first frame update

    [SerializeField] private Collider[] arenaBoundaries;

    [SerializeField] private GameObject Boss;

    [SerializeField] private GameObject music;
    [SerializeField] private GameObject transitionEffect;
    void Start()
    {
        SetArenaBoundariesActive(false);
    }

    // Update is called once per frame

    // none of this code here should be an update. We shouldnt check this every time. Too late to fix now.
    void Update()
    {
        if (Boss.GetComponent<BossBehaviour>().defeated == true)
        {
            SetArenaBoundariesActive(false);
            // move to next scene 
            StartCoroutine(BossDefeated());

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Boss.SetActive(true);
            BossHealth.SetActive(true);
            SetArenaBoundariesActive(true);
            music.GetComponent<MusicHandler>().changeMusic(2);
            music.GetComponent<MusicHandler>().changeVolume(0.5f);
        }
    }

    private void SetArenaBoundariesActive(bool active)
    {
        foreach (Collider boundary in arenaBoundaries)
        {
            boundary.enabled = active;
        }
    }

    public IEnumerator BossDefeated()
    {
        music.GetComponent<MusicHandler>().changeMusic(0);
        music.GetComponent<MusicHandler>().changeVolume(0.6f);
        PlayerPrefs.DeleteKey("RespawnX");
        PlayerPrefs.DeleteKey("RespawnY");
        PlayerPrefs.DeleteKey("RespawnZ");

        ReduceBitDepth reduceBitDepthComponent = transitionEffect.GetComponent<ReduceBitDepth>();
        reduceBitDepthComponent.ReduceScreenBitDepth();


        yield return new WaitForSeconds(5);

        if (SceneManager.GetActiveScene().buildIndex == 6) {
                // beat the game!
                SceneManager.LoadScene(8);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
