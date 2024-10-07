using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 10f;
    public float smoothTurn = 0.1f;
    private float turnSmoothVelocity;

    private PlayerEffects playerEffects;

    private float vspeed = 0; // vertical speed

    public Vector3 lastDirection; // the last direction the player moved

    public float gravity = 9.8f; // the force of gravity after a player jumps

    private PlayerState playerState;

    void Start()
    {
        playerEffects = GetComponent<PlayerEffects>();
        playerState = GetComponent<PlayerState>();   
        playerState.SetState("Idle");
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothTurn);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (playerState.GetState() != "DiggingUp") {
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }

            lastDirection = moveDir.normalized;

            // play centipede crawl audio
            playerEffects.PlayCrawlSound();
        } 
        else
        {
            lastDirection = Vector3.zero;
        }

        if (controller.isGrounded)
        {
            vspeed = -0.5f; // small downward force to keep the player grounded
        }
        else
        {
            vspeed -= gravity * Time.deltaTime;
        }

        Vector3 verticalMovement = new Vector3(0, vspeed, 0);
        controller.Move(verticalMovement * Time.deltaTime);
    }
}