using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCharacterController : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    private Vector3 characterRotation;

    private Rigidbody characterRb;

    [SerializeField] private float turnRateX;
    [SerializeField] private float turnRateY;

    private float mouseXInput;
    private float mouseYInput;

    private float walkSpeed = 5f;
    private float runSpeed = 10f;
    private float desiredSpeed;
    private float jumpVelocity = 10f;

    private bool isGrounded;
    
    private void Start()
    {
        characterRb = this.GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        MoveCharacter();
        SetMovementSpeed();
        
        RotateCharacter();
        PivotCamera();
        
        Jump();
    }
    
    private void SetMovementSpeed()
    {
        desiredSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
    }
    
    private void MoveCharacter()
    {
        if(IsGrounded())
        {
            characterRb.velocity = transform.forward  * (desiredSpeed * Input.GetAxis("Vertical"));
            characterRb.velocity += transform.right  * (desiredSpeed * Input.GetAxis("Horizontal"));
        }
    }
    
    private float CalculateMouseXDelta()
    {
        mouseXInput = Input.GetAxis("Mouse X") * turnRateX * Time.deltaTime;

        return mouseXInput;
    }

    private float CalculateMouseYDelta()
    {
        mouseYInput -= Input.GetAxis("Mouse Y") * turnRateY * Time.deltaTime;
        
        return mouseYInput;
    }
    
    private void PivotCamera()
    {
        cameraPivot.localRotation = Quaternion.Euler(CalculateMouseYDelta(), 0f, 0f);
    }

    private void RotateCharacter()
    {
        characterRotation = new Vector3(0, CalculateMouseXDelta(), 0);
        this.transform.Rotate(characterRotation);
    }

    private bool IsGrounded()
    {
        isGrounded = Physics.Raycast(this.transform.position, Vector3.down, 1.005f);

        return isGrounded;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, this.transform.position.z);
            characterRb.velocity += new Vector3(0,jumpVelocity,0);
        }
    }
}
