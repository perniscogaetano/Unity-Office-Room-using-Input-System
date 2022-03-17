
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MouseLook : MonoBehaviour
{
    private PlayerInputaction playerInputaction;
    private float mouseSensitivity = 100f;
    private Vector2 mouseLook;
    private float xRotation = 0f;
    private Transform playerBody;

    void Awake()
    {
        playerBody = transform.parent;
        playerInputaction = new PlayerInputaction();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Look();
    }

    private void Look()
    {
        mouseLook = playerInputaction.Player.LookAround.ReadValue<Vector2>();
        float mouseX = mouseLook.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseLook.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void OnEnable()
    {
        playerInputaction.Enable();
    }


    private void OnDisable()
    {
        playerInputaction.Disable();
    }
}