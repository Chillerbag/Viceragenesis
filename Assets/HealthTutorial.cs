using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HealthTutorial : MonoBehaviour
{
    
    public PlayableDirector director;
    private TimelineController timelineController;
    void Start()
    {
        timelineController = director.GetComponent<TimelineController>();
        
    }
    // when player collides with my trigger, call timelineController method
    void OnDisable()
    {
        timelineController.ResumeTimeline(); 
    }
}
