using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// basicPlayerMovement.cs - Written by Ethan Hawkins

// what is this script for?
    /* 
    This script is responsible for the player's movement and interactions with the environment.
    The player can move around the environment, dig underground, and attack enemies.
    The player can also take damage and lose health, which results in the destruction of nodes.
    The player can also bounce off enemies when attacking them.
    The player can also move to the top of diggable objects by raycasting and moving to the top marker.
    */

// TODOs:
    /*
    - Refactor the code to be more readable and maintainable.
    - Add comments to explain the code.
    - make the player's movement more fluid and responsive (specifically digging and attacking).
    - change all public variables to private and use properties to access them (getters and setters).
    - fix bouncing off enemies to actually work.
    */

public class basicPlayerMovement : MonoBehaviour
{
    // ----------- Public Variables -----------
    public CharacterController controller; // attached player controller component
    public Transform cam; // third person camera
    public float speed = 6f; // how fast the player moves
    public float smoothTurn = 0.1f; // the smoothness of player turning relative to the camcera
    public GameObject playerModel; // the player rig 
    public float gravity = 9.8f; // the force of gravity after a player jumps
    public LayerMask groundLayer; // the layer that we define as the ground for player collision
    public int playerLayer; // the layer the player exists on for object collisions
    public int ignoreCollisionLayer; // the layer we move to when underground
    private float turnSmoothVelocity; 
    [SerializeField] private AudioClip[] diggingAudioClips; // for digging soundFX
    // for walking soundFX
    [SerializeField] private AudioClip[] crawlAudioClips; // for when the player is moving
    public string State = "Idle"; // state of the player for external objects to use
    public ParticleSystem undergroundEffect; // particle effect for being underground
    public ParticleSystem attackEffect; // particle effect for dashing through the air ?

    [SerializeField] private Slider cooldownSlider; // the slider for the player's cooldown for digging again 

    // ----------- Private Variables -----------
    private float vspeed = 5; // how fast the player falls
    private float timeInvisible = 1.5f; // how long the player is invisible when digging
    private float undergroundBuffer = 2.0f; // how long the player must wait to dig again
    private bool isUnderground = false; // is the player underground?
    private Vector3 lastDirection; // the last direction the player moved
    private int noCollisionLayer = 11; // the layer the player moves to when underground
    private int BulletLayer = 14; // the layer of bullets (dont collide with when underground)
    private int originalLayer; // the original layer of the player
    private Animator rigAnimator; // the animator for the player rig

    // ----------- Unity Functions -----------

    // start()
    // what do we do here? - set the player layer, turn off the underground effect, and get the player rig animator.
    void Start()
    {
        playerLayer = gameObject.layer; 
        var emission = undergroundEffect.emission; // figure out how to not store this as var. awful.
        emission.enabled = false; // turn off the underground effect to start
        rigAnimator = playerModel.GetComponent<Animator>(); // get the animator from the player rig
        cooldownSlider.value = 0;
    }

    // update()
    // what do we do here? - check for player input, move the player, check for collisions, and play soundFX.
    void Update()
    {
        // make the cooldown slider invisible if 0 
        if (cooldownSlider.value == 0)
        {
            cooldownSlider.gameObject.SetActive(false);
        }
        else
        {
            cooldownSlider.gameObject.SetActive(true);
        }

        var emission = undergroundEffect.emission; // TODO: ugly. Poor naming too. 

        // check health and destroy a node if health is decreased
        OnHealthLoss();

        // check if the player is underground and decrement the buffer TODO: combine this into the cooldown slider
        if (undergroundBuffer > 0)
        {
            undergroundBuffer -= Time.deltaTime;
            cooldownSlider.value = undergroundBuffer;
        }

        // get player input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // normalize the direction of player input

        // move the player if we are inputting a direction
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurn);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (isUnderground)
            {
                // Detect the surface normal below the player
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -transform.up, out hit, 2.0f, groundLayer))
                {
                    // Project the movement direction onto the surface
                    moveDir = Vector3.ProjectOnPlane(moveDir, hit.normal).normalized;
                    // Adjust the player's position to adhere to the surface
                    transform.position = hit.point + hit.normal * (controller.height / 2);
                }
                else {
                    moveDir = Vector3.zero;
                }
            }

            if (State != "DiggingUp") {
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            lastDirection = moveDir.normalized;

            // play centipede crawl audio
            SoundFXManager.instance.PlayRandomSoundFXClip(crawlAudioClips, transform, 0.2f);
        }
        else
        {
            lastDirection = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.Space) && undergroundBuffer <= 0 && controller.isGrounded && !isUnderground)
        {
            rigAnimator.SetTrigger("startDig");
            //SetVisibility(false);
            SetLayerCollision(false);
            isUnderground = true;
            timeInvisible = 1.5f;

            // play digging audio
            SoundFXManager.instance.PlayRandomSoundFXClip(diggingAudioClips, transform, 0.2f);
        }
        if (isUnderground)
        {
            emission.enabled = true;
            timeInvisible -= Time.deltaTime;
            if (timeInvisible <= 0)
            {
                rigAnimator.SetTrigger("endDig");
                emission.enabled = false; 
                //SetVisibility(true);
                SetLayerCollision(true);
                isUnderground = false;
                undergroundBuffer = 2.0f;
                cooldownSlider.value = undergroundBuffer;
                MoveToTopMarker();
                StartCoroutine(DolphinDive());
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && isUnderground)
        {
            rigAnimator.SetTrigger("endDig");
            emission.enabled = false; 
            //SetVisibility(true);
            SetLayerCollision(true);
            isUnderground = false;
            undergroundBuffer = 2.0f;
            cooldownSlider.value = undergroundBuffer;
            MoveToTopMarker();
            StartCoroutine(DolphinDive());
        }

        if (controller.isGrounded)
        {
            vspeed = 0;
        }

        vspeed -= gravity * Time.deltaTime;
        direction.y = vspeed;
        controller.Move(direction * Time.deltaTime);
    }

void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.tag == "Enemy")
    {
        if (State == "Attacking")
        {
            Vector3 reflection = Vector3.Reflect(lastDirection, collision.contacts[0].normal);
            collision.gameObject.GetComponent<EnemyHealthManager>().DamageToEnemy(1);
            // bounce back after attacking
            StartCoroutine(BounceBack(reflection.normalized));
        }
    }
}

private IEnumerator BounceBack(Vector3 bounceBack)
{
    float bounceDuration = 0.5f; // Duration of the bounce back
    float elapsedTime = 0f;
    Rigidbody rb = GetComponent<Rigidbody>();

    // Gradually apply a backward force over the duration of the bounce
    while (elapsedTime < bounceDuration)
    {
        float t = elapsedTime / bounceDuration;
        rb.AddForce(bounceBack * 20 * (1 - t));

        elapsedTime += Time.deltaTime;
        yield return null;
    }
}

private void SetLayerCollision(bool enabled)
{
    Physics.IgnoreLayerCollision(playerLayer, ignoreCollisionLayer, !enabled);
    Physics.IgnoreLayerCollision(playerLayer, BulletLayer, !enabled);
}

private IEnumerator DolphinDive()
{
    var attackEmission = attackEffect.emission;
    attackEmission.enabled = true;
    State = "Attacking";
    float diveDuration = 0.7f; // Duration of the dive
    float elapsedTime = 0f;
    Vector3 initialVelocity = lastDirection * speed + Vector3.up * 15;

    //var SpeedEffectController = SpeedEffect.GetComponent<MaterialPropertyController>();
    //SpeedEffectController.isEnabled = true;
    //SpeedEffectController.UpdateShaderProperties();

    // while only works here because coroutines are frame dependent (independent of update). 
    while (elapsedTime < diveDuration)
    {
        controller.Move(initialVelocity * Time.deltaTime);
        initialVelocity += Vector3.down * gravity * Time.deltaTime;
        elapsedTime += Time.deltaTime;
        yield return null;
    }
    //SpeedEffectController.isEnabled = false;
    //SpeedEffectController.UpdateShaderProperties();
    attackEmission.enabled = false;
    State = "Idle";

}

// to do: clean this up. It sucks. 
private void OnHealthLoss()
{
    PlayerHealth health = GetComponent<PlayerHealth>();
    String NodeToDestroy = "Node." + health.currentHealth;
    Destroy(GameObject.Find(NodeToDestroy));

    if (health.currentHealth == 2) {
        Destroy(GameObject.Find("Cube.018"));
        Destroy(GameObject.Find("Cube.017"));
        Destroy(GameObject.Find("Cube.016"));
        Destroy(GameObject.Find("Cube.015"));
        Destroy(GameObject.Find("Cube.014"));
        Destroy(GameObject.Find("Cube.013"));
    }

    if (health.currentHealth == 1) {
        Destroy(GameObject.Find("Cube.012"));
        Destroy(GameObject.Find("Cube.011"));
        Destroy(GameObject.Find("Cube.010"));
        Destroy(GameObject.Find("Cube.009"));
        Destroy(GameObject.Find("Cube.008"));
        Destroy(GameObject.Find("Cube.007"));
    }
    
}

private void MoveToTopMarker()
{
    RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.up);
    if (hits.Length > 0)
    {
        // Find the highest hit point
        RaycastHit highestHit = hits[0];
        foreach (RaycastHit hit in hits)
        {
            if (hit.point.y > highestHit.point.y)
            {
                highestHit = hit;
            }
        }
        Transform topMarker = highestHit.collider.transform.Find("TopMarker");
        if (topMarker != null)
        {
            originalLayer = gameObject.layer;
            gameObject.layer = noCollisionLayer;
            // Move the player to the position of the marker object
            StartCoroutine(SmoothDigUp(topMarker.position.y));
            //controller.Move(topMarker.position.y * Vector3.up);
            //gameObject.layer = originalLayer;
        }       
    }
}


private IEnumerator SmoothDigUp(float endPosition)
{
    float duration = 0.6f; // Duration of the movement in seconds
    float elapsedTime = 0f;
    float startPosition = transform.position.y;

    while (elapsedTime < duration)
    {
        State = "DiggingUp";
        float newY = Mathf.Lerp(startPosition, endPosition, elapsedTime / duration);
        Vector3 newPosition = new Vector3(transform.position.x, newY, transform.position.z);
        controller.Move(newPosition - transform.position); // Move the player to the new position

        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // Ensure the player reaches the exact end position
    Vector3 finalPosition = new Vector3(transform.position.x, endPosition, transform.position.z);
    controller.Move(finalPosition - transform.position);
    State = "Idle";

    gameObject.layer = originalLayer;
}
}