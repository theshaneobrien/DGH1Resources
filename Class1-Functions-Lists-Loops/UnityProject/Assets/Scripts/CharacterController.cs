using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    private float walkSpeed = 5f;
    private float runSpeed = 10f;
    private float desiredSpeed = 0;
    private float jumpForce = 5f;

    public Rigidbody characterRb;

    private void Start()
    {
        desiredSpeed = walkSpeed;
    }

    private void Update()
    {
        MovePlayer();
        DetectWalkRun();
        Jump();
    }

    private void DetectWalkRun()
    {
       // Detect a key being HELD by the user
        //Input.GetKey
       // Detct when a users presses a key down once
        //Input.GetKeyDown
       // Detect when a user lets go of a key that was pressed
        //Input.GetKeyUp 

        if(Input.GetKeyDown(KeyCode.V))
        {
            desiredSpeed = runSpeed;
        }

        if(Input.GetKeyUp(KeyCode.V))
        {
            desiredSpeed = walkSpeed;
        }

    }

    private void MovePlayer()
    {
        //If we detect any input move the player
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            characterRb.velocity = new Vector3(
                    Input.GetAxis("Horizontal") * desiredSpeed,
                    0.0f,
                    Input.GetAxis("Vertical") * desiredSpeed);
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            characterRb.velocity = new Vector3(0.0f, jumpForce, 0.0f);
        }
    }
    
}
