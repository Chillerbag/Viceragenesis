using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    private PlayableDirector director;
    private bool isPaused = false;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void PauseTimeline()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        isPaused = true;
    }
    
    public void ResumeTimeline()
    {
        if (isPaused)
        {
            director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            isPaused = false;
        }
    }
}