using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using UnityEngine.UIElements;
public class ChargeAttack: MonoBehaviour, IAttackable
{

    public NavMeshAgent agent;

    public Transform player; // Reference to the player's transform
    public float speed = 20f; 
    public float angularSpeed = 5f; // degrees per second

    public Renderer rend;

    public float acceleration = 100f;
    public int damage = 1; 
    public Rigidbody rb;
    public float chargeTime = 10f;
    private Boolean isCharging ;
    private float chargingTimer = 0;
    private float restSpeed;
    private float restAngularSpeed;
    private float restAcceleration;
    private float MINIMUM_SPEED =1f;
     Color inactiveColour;
    public Color activeColour;


    void Start()
    {   
        inactiveColour = rend.material.color;
        player =  GameObject.Find("Player").transform;
        chargingTimer = 0f;
        isCharging = false;
        restSpeed = agent.speed;
        restAngularSpeed = agent.angularSpeed;
        restAcceleration =agent.acceleration;
    }

    void Update()
    {
        if (chargingTimer >0){
            FaceTarget();
            agent.SetDestination(player.position);
            agent.angularSpeed = angularSpeed;
            agent.speed = 30;
            chargingTimer -= Time.deltaTime;
        } else{
            if (isCharging)
            {
                FinishCharging();
            }

        }

    
    }

    public void Attack()
    {
        Debug.Log("charging");

        isCharging =true;
        chargingTimer = chargeTime;
        agent.speed = speed;
        agent.acceleration = acceleration;
        agent.angularSpeed = angularSpeed;

        Debug.Log(agent.angularSpeed);
        rend.material.color = activeColour;

    }

    void OnCollisionEnter(Collision collision)
    {   
        if(isCharging){
            // Try to get the PlayerHealth component from the collided object
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            FinishCharging();

            if (playerHealth != null)
            {
                // If the component exists, apply damage
                playerHealth.TakeDamage(damage);
            }

        }
    }

    [Obsolete]
    void FinishCharging()
    {
        Debug.Log("FinishCharging");
        chargingTimer =0;
        isCharging =false;
        agent.speed = restSpeed;
        agent.angularSpeed = restAngularSpeed;
        agent.acceleration = restAcceleration;
        agent.ResetPath();
        rend.material.color = inactiveColour;

    }
    void FaceTarget()
{
    if(agent.velocity.magnitude > MINIMUM_SPEED)
    {
        Vector3 direction = agent.velocity;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
}
}

