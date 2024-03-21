using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;

public class PlayerSlowDownMovement : MonoBehaviour
{
    public float speed;

    [SerializeField] public float moveSlowSpeed = 2.5f;

    public Rigidbody rb;

    public bool enter = false;

    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Collider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        speed = rb.velocity.magnitude;

        if (enter)
        {
            speed = rb.velocity.magnitude - moveSlowSpeed;
        }
        if (!enter)
        {
            speed = rb.velocity.magnitude;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ShadowZone"))
        {
            enter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ShadowZone"))
        {
            enter = false;
        }
    }
}
