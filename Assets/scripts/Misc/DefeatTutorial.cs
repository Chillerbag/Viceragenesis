using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DefeatTutorial : MonoBehaviour
{

    private EnemyHealthManager enemyHealthManager;
    public GameObject successText;
    public PlayableDirector director;
    private TimelineController timelineController;
    void Start()
    {
        enemyHealthManager = GetComponent<EnemyHealthManager>();
        successText.SetActive(false);
        timelineController = director.GetComponent<TimelineController>();
        
    }
    // when player collides with my trigger, call timelineController method
    void OnDisable()
    {
        if (enemyHealthManager.getIsDead())
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
