using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    private string State;
    void Start()
    {
        State = "Idle";
    }

    public string GetState() {
        return State;
    }   

    public void SetState(string newState) {
        State = newState;
    }
}
