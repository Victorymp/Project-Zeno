using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform ori;

    float xRotation;
    float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input 
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // transform.rotation = Quaternion.Euler(xRotation, ori.rotation.eulerAngles.y, 0);
        ori.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookValue = context.ReadValue<Vector2>();
        xRotation -= lookValue.y * sensY;
        yRotation += lookValue.x * sensX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        ori.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}
