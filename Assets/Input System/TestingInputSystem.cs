using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    private Rigidbody cylinderRigidbody;
    private PlayerInput playerInput;
    private PlayerInputaction playerInputaction;

    private void Awake()
    {
        cylinderRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInputaction = new PlayerInputaction();
        playerInputaction.Player.Enable();
        playerInputaction.Player.Jump.performed += Jump;
    }  
     
    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
         {
            playerInput.SwitchCurrentActionMap("UI");
            playerInputaction.Player.Disable();
            playerInputaction.UI.Enable();
        }
         if (Keyboard.current.yKey.wasPressedThisFrame)
         {
             playerInput.SwitchCurrentActionMap("Player");
            playerInputaction.UI.Disable();
            playerInputaction.Player.Enable();
         }
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputaction.Player.Movement.ReadValue<Vector2>();
        Debug.Log(inputVector);
        float speed = 2f;
        Vector3 m_Input = new Vector3(inputVector.x, 0, inputVector.y);
        cylinderRigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
    }
   
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("JUMP!");
        if (context.performed)
        {
          Debug.Log("Jump!" + context.phase);
          cylinderRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
    public void Submit(InputAction.CallbackContext context)
    {
        Debug.Log("Submit " + context);
    }
}
