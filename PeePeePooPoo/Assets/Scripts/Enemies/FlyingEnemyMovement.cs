using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 
    GameObject player; 
    Rigidbody rb;
    float timer = 0f;

    public float lookSpeed = 2f; 
    public float delayTime = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = PlayerStats.instance.gameObject;
    }

    private void FixedUpdate()
    {

        //rotate the enemy towards the player
        RotateEnemyTowardsPlayer();

        // calculate the direction towards the player
        Vector3 directionToPlayer = player.transform.position - transform.position;

        // move the enemy towards the player using MoveTowards method
        Vector3 newPosition = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        // set the new position for the enemy's Rigidbody component
        rb.MovePosition(newPosition);
        
    }

    void RotateEnemyTowardsPlayer()
    {
        // increment the timer
        timer += Time.deltaTime;

        // if the delay time has elapsed, rotate towards the target
        if (timer >= delayTime)
        {
            // calculate the direction to the target
            Vector3 directionToTarget = player.transform.position - transform.position;

            // calculate the rotation towards the target
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // smoothly rotate towards the target using Quaternion.Lerp()
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
        }
    }
}
