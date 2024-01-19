using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerUpdated : MonoBehaviour
{
    [SerializeField] public float speed;

    public Transform direction;

    float horizontal;
    float vertical;

    Vector3 moveDirection;

    private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void PlayerMovement()
    {
        moveDirection = direction.forward * vertical + direction.right * horizontal;
        rb.AddForce(moveDirection.normalized * speed * 10f);
    }
}
