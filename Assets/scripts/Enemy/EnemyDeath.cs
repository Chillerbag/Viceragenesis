using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject boss;
        public Color targetColor = Color.black; // Dark color
    public float duration = 2f; // Duration of the transition

    private Renderer objectRenderer;
    private Color initialColor;
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        objectRenderer = GetComponent<Renderer>();

    }
    public void Death(){
        Destroy(gameObject);
    }
  private IEnumerator Die()
    {
        Color initialColor = objectRenderer.material.color; // Save the initial color
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // Increment elapsed time
            // Lerp the color between initial and target colors
            objectRenderer.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / duration);
            yield return null; // Wait for the next frame
        }

        // Ensure the final color is set
        objectRenderer.material.color = targetColor;
    }
}
