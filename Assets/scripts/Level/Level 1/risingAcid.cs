using UnityEngine;
using TMPro;
using System.Collections;

public class RisingAcid : MonoBehaviour
{
    public Transform player;
    public float riseSpeed = 0.015f;
    public float maxHeight = 680f;
    public float triggerHeight = 30f;

    [SerializeField] private TextMeshProUGUI LevelText;

    private bool hasStartedRising = false;

    void Start()
    {
        LevelText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!hasStartedRising && player.position.y > transform.position.y + triggerHeight)
        {
            StartCoroutine(StartRising());
        }

        if (hasStartedRising && transform.position.y < maxHeight)
        {
            transform.position += new Vector3(0, riseSpeed, 0);
        }
    }

    IEnumerator StartRising()
    {
        hasStartedRising = true;
        LevelText.text = "Acid is rising! Run!";
        LevelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        LevelText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(3);
        }
    }
}