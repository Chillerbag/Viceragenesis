using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmStrech : Bouncing
{   
    private bool isBeating = false;
    
    private Vector3 startScale;
    public float maxSize = 2f;

    private float currentSize;
    public float increaseSpeed = 1f;

    public float stopTime = 0.75f;
    
    public float minSize =1f;

    void Start()
    {
        startScale = transform.localScale;
    }
    
    void Update()
    {   
        if(!isBeating & active){
            StartCoroutine(startStretch());
        }
    }

    public IEnumerator startStretch() {
        isBeating = true;

        while (currentSize < maxSize){
            currentSize += increaseSpeed*Time.deltaTime ;
            Vector3 newScale = startScale;
            newScale.y = startScale.y * currentSize;
            transform.localScale =newScale;

            yield return null;
        }
        yield return new WaitForSeconds(stopTime);

        while (currentSize > minSize){
            currentSize -= increaseSpeed*Time.deltaTime ;
            Vector3 newScale = startScale;
            newScale.y = startScale.y * currentSize;
            transform.localScale =newScale;

             yield return null;
        }
        currentSize = minSize;
        yield return new WaitForSeconds(stopTime);
        isBeating = false;

    }

}
