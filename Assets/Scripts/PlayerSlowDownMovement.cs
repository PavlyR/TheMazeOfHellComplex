using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlowDownMovement : MonoBehaviour
{
    public float moveSpeed;

    [SerializeField] public float moveSlowSpeed = 2.5f;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = rb.velocity.magnitude;
    }
}
