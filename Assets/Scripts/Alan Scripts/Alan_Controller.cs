using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Alan_Controller : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject playerRef;
    public bool agro;
    public float agroTimer;
    private float timeSinceSpotted = 0f;

    [SerializeField]
    private Transform patrol;

    [SerializeField]
    private LineRenderer line;

    /// <summary>
    /// This script is to test the navmesh, it will be incoperated into the Alan mov once the floor is completed.
    /// </summary>

    void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (agro == true)
        {
            agent.SetDestination(playerRef.transform.position);
            timeSinceSpotted -= Time.deltaTime;
            line.SetPosition(0, transform.position); 
            DrawPath(agent.path);

            if (timeSinceSpotted <= 0)
            {
                agro = false;
                timeSinceSpotted = agroTimer;

            }
        }
        else
        {
            agent.SetDestination(patrol.position);
            line.SetPosition(0, transform.position); //set the line's origin
            DrawPath(agent.path);
        }
    }

    public void Spotted(bool x)
    {
        agro = x;
        timeSinceSpotted = agroTimer;


    }

    private void DrawPath(NavMeshPath path)
    {
       
        if (path.corners.Length < 2)
        { //if the path has 1 or no corners, there is no need
            return;


        }
        line.SetVertexCount(path.corners.Length); //set the array of positions to the amount of corners
        for (int i = 1; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
        }

        RaycastHit hit;
        if (Physics.Linecast(path.corners[0], path.corners[1], out hit)) //Using the path we compare if there is an object in the way. 
        {
            float dist = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);
            if (hit.collider.gameObject.TryGetComponent(out IInteractable interactObj)&& dist <= 10)
            {
                interactObj.Interact(); //calls the interact of the object being looked at.
            }
        }

    }
}
