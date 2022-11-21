using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// You can see a fully commented version of this file at the following location: https://gist.github.com/theshaneobrien/6a234135b9be1823fc089af4866498c3
public class FpsCharacterController : MonoBehaviour
{
    [SerializeField] private Transform cameraPivot;
    private Vector3 characterRotation;
    
    private Rigidbody characterRb;

    private float mouseXInput;
    private float mouseYInput;
    
    [SerializeField] private float turnRateX = 150;
    [SerializeField] private float turnRateY = 150;
    
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpVelocity = 10f;
    
    private float desiredSpeed;

    private bool isGrounded;

    [SerializeField] private GameStateManager gameState;

    private void Start()
    {
        characterRb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (gameState.playerIsReady == true)
        {
            MoveCharacter();
            SetMovementSpeed();

            RotateCharacter();
            PivotCamera();

            Jump();
        }

    }
    
    private void SetMovementSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            desiredSpeed = runSpeed;
        }
        else
        {
            desiredSpeed = walkSpeed;
        }
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
        mouseXInput = Input.GetAxis("Mouse X") * Time.deltaTime * turnRateX;
        
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
