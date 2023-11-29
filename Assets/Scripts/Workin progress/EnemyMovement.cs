using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float EnemySpeed = 5.0f;

    public GameObject Player;
    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            //transform.position = Player.transform.position + transform.forward;
            Movement();
        }
    }

    void Movement()
    {
        transform.Translate(Vector3.forward *  EnemySpeed * Time.deltaTime);

    }
}
