using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    
    //These are our input variables
    private PlayerControls playerInput;
    private Vector2 playerMovementVector;
    private Vector2 playerLookVector;

    private void Awake()
    {
        playerInput = new PlayerControls();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.PlayerInGame.Movement.performed += OnMovementPerformed;
        playerInput.PlayerInGame.Movement.canceled += OnMovementCancelled;

        playerInput.PlayerInGame.Look.performed += OnLookPerformed;
        playerInput.PlayerInGame.Look.canceled += OnLookCancelled;

        playerInput.PlayerInGame.Jump.performed += OnJumpPerformed;

        playerInput.PlayerInGame.Sprint.performed += OnSprintPerformed;
        playerInput.PlayerInGame.Sprint.canceled += OnSprintCanceled;

    }

    private void OnDisable()
    {
        playerInput.Disable();
        playerInput.PlayerInGame.Movement.performed -= OnMovementPerformed;
        playerInput.PlayerInGame.Movement.canceled -= OnMovementCancelled;
        
        playerInput.PlayerInGame.Look.performed -= OnLookPerformed;
        playerInput.PlayerInGame.Look.canceled -= OnLookCancelled;

        playerInput.PlayerInGame.Jump.performed -= OnJumpPerformed;
        
        playerInput.PlayerInGame.Sprint.performed -= OnSprintPerformed;
        playerInput.PlayerInGame.Sprint.canceled -= OnSprintCanceled;
    }

    private void Start()
    {
        characterRb = this.GetComponent<Rigidbody>();
        desiredSpeed = walkSpeed;
    }

    private void Update()
    {
        if (GameStateManager.Instance.GetPlayerIsReady() == true && GameStateManager.Instance.GetPlayerWon() == false)
        {
                MoveCharacter();

                RotateCharacter();
                PivotCamera();
        }
    }
    
    private void MoveCharacter()
    {
        if(IsGrounded())
        {
            characterRb.velocity = transform.forward  * (desiredSpeed * playerMovementVector.y);
            
            characterRb.velocity += transform.right  * (desiredSpeed * playerMovementVector.x);
        }
    }
    
    private float CalculateMouseXDelta()
    {
        mouseXInput = playerLookVector.x * Time.deltaTime * turnRateX;
        
        return mouseXInput;
    }

    private float CalculateMouseYDelta()
    {
        mouseYInput -= playerLookVector.y * turnRateY * Time.deltaTime;
        mouseYInput = Mathf.Clamp(mouseYInput, -90, 90);
        return mouseYInput;
    }
    
    private void PivotCamera()
    {
        cameraPivot.localRotation = Quaternion.Euler(Mathf.Clamp(CalculateMouseYDelta(), -90.0f, 90.0f), 0f, 0f);
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
    
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        playerMovementVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        playerMovementVector = Vector2.zero;
    }

    private void OnLookPerformed(InputAction.CallbackContext value)
    {
        playerLookVector = value.ReadValue<Vector2>();
    }

    private void OnLookCancelled(InputAction.CallbackContext value)
    {
        playerLookVector = Vector2.zero;
    }

    private void OnSprintPerformed(InputAction.CallbackContext value)
    {
        desiredSpeed = runSpeed;
    }
    
    private void OnSprintCanceled(InputAction.CallbackContext value)
    {
        desiredSpeed = walkSpeed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext value)
    {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, this.transform.position.z);
            
            characterRb.velocity += new Vector3(0,jumpVelocity,0);
    }
}
