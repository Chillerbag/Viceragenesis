using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletWithVarySpeed : MonoBehaviour
{
    public int damage = 1; 
    public float lifeTime = 3f; 
    public float minSpeed = 1f;
    public float maxSpeed = 40f;

    public float acceleration = 100f;
    public int isIncreasing = 1;

    Rigidbody rb ;
    void Start()
    {   GetComponent<BulletWithVarySpeed>().lifeTime = 10f;
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);  
        StartCoroutine(Travel());
    }

private IEnumerator Travel()
{
    while (true) // Keep running the logic indefinitely until manually stopped
    {
        // Update the speed
        float newSpeed = rb.velocity.magnitude + acceleration * Time.deltaTime * isIncreasing;

        // Ensure the speed stays within the defined bounds
        if (newSpeed >= minSpeed && newSpeed <= maxSpeed)
        {
            // Move the object by adjusting its velocity
            rb.velocity = transform.forward * newSpeed;
            Stretch(newSpeed);

        }
        else
        {
            // Reverse direction and recalculate speed when limits are reached
            isIncreasing *= -1;
            //Debug.Log("change direction");
            newSpeed = rb.velocity.magnitude + acceleration * Time.deltaTime * isIncreasing;
            rb.velocity = transform.forward * newSpeed;
            Stretch(newSpeed);
            // Wait for 1 second before continuing
            yield return new WaitForSeconds(0.5f);
        }


        //Debug.Log("Current Speed: " + rb.velocity.magnitude+ "direction"+isIncreasing);

        // Yield for the next frame
        yield return null;
    }
}


    private void Stretch(float magnitude){
        float stretchFactorH = (magnitude - minSpeed)/(maxSpeed-minSpeed)+0.5f;
        float stretchFactorV = (magnitude - minSpeed)/(maxSpeed-minSpeed)*2+0.5f;
        Vector3 newScale = new Vector3(1/stretchFactorH, 1/stretchFactorH, stretchFactorV);
        transform.localScale = newScale;
        //Debug.Log("Current Stretch Factor: " + stretchFactor);

    }


    void OnCollisionEnter(Collision collision)
    {
        // Try to get the PlayerHealth component from the collided object
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // If the component exists, apply damage
            playerHealth.TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Boss") {
            // dont collide with boss
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }

        if (collision.gameObject.tag == "ArenaWall") {
            // dont collide with arena walls
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        if (collision.gameObject.tag == "Enemy") {
            // dont collide with arena walls
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }


        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Boss"&& collision.gameObject.tag != "Enemy") {
            Destroy(gameObject);
        }
    }

        /*
        switch (tag)
        {   
            case "Boss" :
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
                break;
            case "Bullet" :
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
                break;
            case "ArenaWall" :
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
                break;
            case "Enemy" :
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
                break;
            case "Player" :
                Destroy(gameObject);
                break;
            default:
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
                break;


        }
        */
    
}