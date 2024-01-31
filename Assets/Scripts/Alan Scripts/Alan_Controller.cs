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
    private float timeSinceSpotted;

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
        }
    }

    public void Spotted(bool x)
    {
        agro = x;
        timeSinceSpotted = 0;
        Search();
    }

    private void Search() //When this is called
    {
        if(agro == true)
        {
            timeSinceSpotted += Time.deltaTime;

            if(timeSinceSpotted >= agroTimer)
            {
                agro = false;
                timeSinceSpotted = 0;
            }
        }
    }
}
