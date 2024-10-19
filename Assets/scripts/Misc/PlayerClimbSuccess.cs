using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerClimbSuccess : MonoBehaviour
{

    public PlayableDirector director;

    private TimelineController timelineController;

    public GameObject successText;


    void Start()
    {
        successText.SetActive(false);
        timelineController = director.GetComponent<TimelineController>();
        
    }
    // when player collides with my trigger, call timelineController method
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           successText.SetActive(true);
            StartCoroutine(HideText());
           timelineController.ResumeTimeline(); 

        }
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(3);
        successText.SetActive(false);
    }

}
