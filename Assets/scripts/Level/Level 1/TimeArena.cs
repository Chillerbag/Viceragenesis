using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeArena : MonoBehaviour
{

    public bool timeActive = false;

    [SerializeField] private TextMeshProUGUI TimerText; 
    // Start is called before the first frame update

    [SerializeField] private Collider[] arenaBoundaries;

    [SerializeField] private GameObject TimeEmitter;
    [SerializeField] private GameObject music;

    private MusicHandler musicHandler;



    public float timeLeft = 20.0f;
    void Start()
    {   
        SetArenaBoundariesActive(false);
        TimeEmitter.SetActive(false);
         musicHandler = music.GetComponent<MusicHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeActive) {
            timeLeft -= Time.deltaTime;
            TimerText.text = "Time Left: " + timeLeft.ToString("F2");
            if (timeLeft <= 0) {
                timeActive = false;
                TimerText.gameObject.SetActive(false);
                Destroy(TimeEmitter);
                SetArenaBoundariesActive(false);
                musicHandler.changeMusic(0);
                musicHandler.changeVolume(0.6f);
                // probably shouldnt do this. 
                gameObject.SetActive(false);
            }
        }
        // if boss dies take down the collider boundaries
        if (TimeEmitter == null ) {
            SetArenaBoundariesActive(false);
        }
        
    }


    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            timeActive = true;
            TimeEmitter.SetActive(true);
            TimerText.gameObject.SetActive(true);
            music.GetComponent<MusicHandler>().changeMusic(1);
            music.GetComponent<MusicHandler>().changeVolume(0.3f);
            SetArenaBoundariesActive(true);
            
        }
    }

    private void SetArenaBoundariesActive(bool active)
    {
        foreach (Collider boundary in arenaBoundaries)
        {
            boundary.enabled = active;
        }
    }
}
