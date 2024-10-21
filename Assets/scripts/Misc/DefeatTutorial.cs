using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DefeatTutorial : MonoBehaviour
{

    private EnemyHealthManager enemyHealthManager;
    public PlayableDirector director;
    private TimelineController timelineController;
    void Start()
    {
        enemyHealthManager = GetComponent<EnemyHealthManager>();
        timelineController = director.GetComponent<TimelineController>();
        
    }
    // when player collides with my trigger, call timelineController method
    void OnDisable()
    {
        if (enemyHealthManager.getIsDead())
        {
            timelineController.ResumeTimeline(); 
        }
    }


}
