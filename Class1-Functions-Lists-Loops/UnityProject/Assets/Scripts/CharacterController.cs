using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float desiredSpeed = 0;
    [SerializeField] private float jumpForce = 5f;

    private float cameraXRotation = 0;

    [SerializeField] private float cameraSensitivity = 20f;

    [SerializeField] private Transform cameraTransform;

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
        HandleCameraRotation();
    }

    private void HandleCameraRotation()
    {
        //We are rotating the transform of the gameObject this script is attached to
        //We are using the Y axis of the gameObject to rotate on the spot
        // We are using the mouse's X movement as the input for this
        this.transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime, 0.0f));

        //We are doing the same again for the Camera's transform
        //Only this time we are using the mouse's Y movement as the input
        cameraTransform.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * -1 * cameraSensitivity * Time.deltaTime, 0.0f, 0.0f));
        
        //TODO: Make sure the camera cannot rotate more that 90 degrees in either direction
        Mathf.Clamp(cameraTransform.rotation.x, -90, 90);
    }

    private void DetectWalkRun()
    {
        //We've configured a "Run" input in the Unity Input Manager
        //If it is pressed, the returned value will be 1
        //Check if it is great than 0 to detect the press
        if(Input.GetAxis("Run") > 0)
        {
            //Set the desiredSpeed to run speed
            desiredSpeed = runSpeed;
        }

        //If it equals zero, the key is not pressed
        if(Input.GetAxis("Run") == 0)
        {
            // Set the walk speed
            desiredSpeed = walkSpeed;
        }
    }

    private void MovePlayer()
    {
        //We are setting the velocity of the player relative to ITs forward postion
        //We are then multiplying that by the desiredSpeed
        //We arethen multiplying that by the Vertical input (-1, 1)
        characterRb.velocity =
            this.transform.forward * desiredSpeed * Input.GetAxis("Vertical");

        //Do the same again for left and right but this ADD it to the
        //Existing velocity so we don't lose forward momentum.
        characterRb.velocity +=
            this.transform.right * desiredSpeed * Input.GetAxis("Horizontal");
    }

    private void Jump()
    {
        //TODO: Fix the jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            characterRb.velocity = new Vector3(0.0f, jumpForce, 0.0f);
        }
    }
    
}
