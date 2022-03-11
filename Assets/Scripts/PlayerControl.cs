using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private float speed = 2f;
    private bool isGrounded;
    private float groundDistance = 0.4f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
    }

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            playerInput.SwitchCurrentActionMap("UI");
            playerInputActions.Player.Disable();
            playerInputActions.UI.Enable();
        }
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            playerInput.SwitchCurrentActionMap("Player");
            playerInputActions.UI.Enable();
            playerInputActions.Player.Enable();
        }
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        //playerRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
        Vector3 m_Input = new Vector3(inputVector.x, 0, inputVector.y);
        m_Input = transform.TransformDirection(m_Input);
        playerRigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("JUMP!");
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
        
    }

    public void Submit(InputAction.CallbackContext context)
    {
        Debug.Log("Submit " + context);
    }
}