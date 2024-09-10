using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeFollowMouse : MonoBehaviour
{

    private Vector3 lastMousePosition;
    [SerializeField] private GameObject[] targets;
    [SerializeField] private float[] weights;
    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }


    // Update is called once per frame
    void Update()
    {
        // Get the current mouse position
        Vector3 currentMousePosition = Input.mousePosition;

        // Calculate the vertical movement of the mouse
        float mouseXDelta = (currentMousePosition.x - lastMousePosition.x) * Time.deltaTime / 100;
        float mouseYDelta = (currentMousePosition.y - lastMousePosition.y) * Time.deltaTime / 100;

        Vector3 aimPosition = new Vector3(mouseXDelta, mouseYDelta, 0);

        // Move the object up if the mouse moved up
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].transform.position += aimPosition * weights[i];
        }

        // Update the last mouse position
        lastMousePosition = currentMousePosition;
    }
}
