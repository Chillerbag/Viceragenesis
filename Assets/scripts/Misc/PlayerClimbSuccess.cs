using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerClimbSuccess : MonoBehaviour
{

    public PlayableDirector director;

    private TimelineController timelineController;


    void Start()
    {
        timelineController = director.GetComponent<TimelineController>();
        
    }
    // when player collides with my trigger, call timelineController method
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           timelineController.ResumeTimeline(); 

        }
    }

}
