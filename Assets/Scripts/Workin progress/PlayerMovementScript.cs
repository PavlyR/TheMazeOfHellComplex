using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody playerRigidbody;

    [SerializeField] private float walkingSpeed = 5.0f;
    [SerializeField] private float runningSpeed = 10.0f;
    //[SerializeField] private float jumpSpeed = 10.0f;
    //[SerializeField] private float gravity = 10.0f;

    public Camera playerCamera;
    public float sensitivity = 5.0f;
    public float cameraAngle = 45.0f;

    public bool canMove = true;
    
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement_forward = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        playerRigidbody.MovePosition(transform.position + movement_forward * Time.deltaTime * walkingSpeed);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -cameraAngle, cameraAngle);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        }

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isRunning)
        {
            playerRigidbody.MovePosition(transform.position + movement_forward * Time.deltaTime * runningSpeed);
        }
    }
}
