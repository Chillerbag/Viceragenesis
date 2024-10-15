using UnityEngine;

public class ScaleWithScreenSize : MonoBehaviour
{
    // the following referenceRes and matches variables are matched with the canvas scaler
    public Vector2 referenceRes = new Vector2(800, 600); 
    // canvas scaler has it set to width so true
    public bool matchesWidth = true; // true to scale by width, false to scale by height
    private Vector3 initialScale;

    void Start()
    {
        // save the initial scale of object
        initialScale = transform.localScale;

        // initial call to update
        UpdateScale();
    }

    void Update()
    {
        // update the scale when the screen size changes
        UpdateScale();
    }

    void UpdateScale()
    {
        // get current screen res
        float width = Screen.width;
        float height = Screen.height;

        // calculate the scaling factor based on the reference resolution
        // found eqn online
        float scaleFactor;

        if (matchesWidth)
        {
            scaleFactor = width / referenceRes.x;
        }
        else
        {
            scaleFactor = height / referenceRes.y;
        }

        // apply scale
        transform.localScale = initialScale * scaleFactor;
    }
}
