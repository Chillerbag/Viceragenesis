using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerDigging : MonoBehaviour
{
    public CharacterController controller;
    public LayerMask groundLayer;
    public GameObject energyBar;
    private bool isUnderground = false;
    private float undergroundBuffer = 1.0f;
    private float timeInvisible = 1.5f;
    private Animator rigAnimator;

    public GameObject playerRig;

    private PlayerEffects playerEffects;

    public int ignoreCollisionLayer; // the layer we move to when underground

    private int BulletLayer = 14; // the layer of bullets (dont collide with when underground

    private PlayerMovement playerMovement;
    private PlayerState playerState;

    public int topLayer;


    void Start()
    {

        rigAnimator = GetComponentInChildren<Animator>();
        energyBar.GetComponent<EnergyBar>().energyLevel = 16;
        playerEffects = GetComponent<PlayerEffects>();
        playerMovement = GetComponent<PlayerMovement>();
        playerState = GetComponent<PlayerState>();
        playerEffects.PlayUndergroundParticles(false);
        playerEffects.PlayAttackParticles(false);
    }

    void Update()
    {


        if (undergroundBuffer > 0)
        {
            undergroundBuffer -= Time.deltaTime;
            energyBar.GetComponent<EnergyBar>().energyLevel = 16 - Mathf.RoundToInt(undergroundBuffer * 16);
        }

        if (Input.GetKey(KeyCode.Space) && undergroundBuffer <= 0 && controller.isGrounded && !isUnderground)
        {
            StartDigging();
            StartCoroutine(ReduceEnergy());
        }

        if (isUnderground)
        {
            timeInvisible -= Time.deltaTime;
            if (timeInvisible <= 0)
            {
                EndDigging();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && isUnderground)
        {
            EndDigging();
        }
    }

    private void StartDigging()
    {
        rigAnimator.SetTrigger("startDig");
        SetLayerCollision(false);
        isUnderground = true;
        timeInvisible = 1.5f;
        playerEffects.PlayDiggingSound();
        playerEffects.PlayUndergroundParticles(true);

    }

    private IEnumerator ReduceEnergy()
    {
        float duration = 0.5f; // Total duration in seconds
        float startTime = Time.time;
        int energyLevel = 16;
        while (energyLevel > 0)
        {
            // Calculate how much time has passed
            float elapsed = Time.time - startTime;

            // Calculate the new value based on elapsed time
            energyLevel = Mathf.Max(0, 16 - Mathf.FloorToInt((elapsed / duration) * 16));
            energyBar.GetComponent<EnergyBar>().energyLevel = energyLevel;
            // Wait for the next frame
            yield return null;
        }


    }

    private void EndDigging()
    {
        rigAnimator.SetTrigger("endDig");
        playerEffects.PlayUndergroundParticles(false);
        isUnderground = false;
        undergroundBuffer = 1.0f;
        //energyBar.GetComponent<EnergyBar>().energyLevel = Mathf.RoundToInt(undergroundBuffer * 16);
        MoveToTopMarker();
        StartCoroutine(DolphinDive());
        SetLayerCollision(true);

    }

    private void SetLayerCollision(bool enabled)
    {
        Physics.IgnoreLayerCollision(gameObject.layer, ignoreCollisionLayer, !enabled);
        Physics.IgnoreLayerCollision(gameObject.layer, BulletLayer, !enabled);
    }

    private void MoveToTopMarker()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.up);
        if (hits.Length > 0)
        {
            Debug.Log("Found " + hits.Length + " hits");
            foreach (RaycastHit hit in hits)
            {
                Transform topMarker = hit.collider.transform.Find("TopMarker");
                if (topMarker != null)
                {
                    Physics.IgnoreLayerCollision(gameObject.layer, topLayer, true);
                    StartCoroutine(SmoothDigUp(topMarker.position.y));
                    return; // Exit the method once the top marker is found and the movement is initiated
                }
            }
        }
    }

    private IEnumerator SmoothDigUp(float endPosition)
    {
        float duration = 0.6f;
        float elapsedTime = 0f;
        float startPosition = transform.position.y;
        playerState.SetState("DiggingUp");

        while (elapsedTime < duration)
        {
            float newY = Mathf.Lerp(startPosition, endPosition, elapsedTime / duration);
            Vector3 newPosition = new Vector3(transform.position.x, newY, transform.position.z);
            controller.Move(newPosition - transform.position);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector3 finalPosition = new Vector3(transform.position.x, endPosition, transform.position.z);
        controller.Move(finalPosition - transform.position);
        Physics.IgnoreLayerCollision(gameObject.layer, topLayer, false);

        StartCoroutine(LaunchUpward());
    }

    private IEnumerator LaunchUpward()
    {
        float launchDuration = 1.5f;
        float elapsedTime = 0f;
        Vector3 launchVelocity = Vector3.up * 35f;

        // Rotate player rig to be vertical
        Quaternion initialRotation = playerRig.transform.rotation;
        Quaternion verticalRotation = Quaternion.Euler(90f, playerRig.transform.eulerAngles.y, playerRig.transform.eulerAngles.z);

        while (elapsedTime < launchDuration)
        {
            controller.Move(launchVelocity * Time.deltaTime);
            launchVelocity += Vector3.down * playerMovement.gravity * Time.deltaTime;

            // Interpolate rotation to vertical
            playerRig.transform.rotation = Quaternion.Slerp(initialRotation, verticalRotation, elapsedTime / launchDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset rotation to normal
        playerRig.transform.rotation = initialRotation;

        // Start falling
        StartCoroutine(FallDown());
    }

    private IEnumerator FallDown()
    {
        float fallDuration = 0.5f;
        float elapsedTime = 0f;
        Vector3 fallVelocity = Vector3.zero;

        while (elapsedTime < fallDuration)
        {
            fallVelocity += Vector3.down * playerMovement.gravity * Time.deltaTime;
            controller.Move(fallVelocity * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerState.SetState("Idle");
    }

    private IEnumerator DolphinDive()
    {
        playerEffects.PlayAttackParticles(true);
        playerState.SetState("Attacking");
        float diveDuration = 0.7f;
        float elapsedTime = 0f;
        Vector3 initialVelocity = playerMovement.lastDirection * playerMovement.speed + Vector3.up * 15;

        while (elapsedTime < diveDuration)
        {
            controller.Move(initialVelocity * Time.deltaTime);
            initialVelocity += Vector3.down * playerMovement.gravity * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerEffects.PlayAttackParticles(false);
        playerState.SetState("Idle");
    }
}