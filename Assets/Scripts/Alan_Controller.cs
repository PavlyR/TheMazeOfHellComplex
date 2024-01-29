using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Alan_Controller : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject playerRef;

    /// <summary>
    /// This script is to test the navmesh, it will be incoperated into the Alan mov once the floor is completed.
    /// </summary>

    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerRef.transform.position);
    }
}
