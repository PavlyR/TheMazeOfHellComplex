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


    public LayerMask ground;
    private Vector3 patrolTarget;
    private bool targetSet = false;

    [SerializeField]
    private int patrolRadius;
    [SerializeField]
    private LineRenderer line;
    private bool aiActive = true;
    [SerializeField] LayerMask wall;
    [SerializeField] NavMeshData walkAble;

    /*[SerializeField] Waypoint hold over
    private Transform[] spawnPoints;

    private int lastPoint;
    */



    /// <summary>
    /// This script is to test the navmesh, it will be incoperated into the Alan mov once the floor is completed.
    /// </summary>

    void Awake()
    {
        //playerRef = GameObject.FindGameObjectWithTag("Player");
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

        GetComponent<Collider>().isTrigger = true;
    }

    
    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        /* Legacy code from when we used gamestates more. Keeping this since we will need a pause menu in the future which could use sections of this. 
        if (state == GameState.GameStart)
        {
            EnemySpawn(); //This could be handled by the gamemanager in the future. When the gamemanager changes it would call Alan_Mov's Enemy Spawn void maybe instead but will work on that later


        }
        else
        {
            aiActive = false;

        }
       */
    }
    

    /*
    private void EnemySpawn() //
    {
        int tempPoint = Random.Range(0, spawnPoints.Length - 1); //Looks at the points that Alan has then selects one at random to spawn at.
        this.transform.position = spawnPoints[tempPoint].position;
        aiActive = true;
    } Player spawn is set right now so Alan will be set as well atleast until we change it. This will be saved just in case of that
    */

    private void Patrol()
    {
        while (!targetSet) SetPatrolPoint(); //While the target isn't set it will try to discover a point within the navmesh

        if (targetSet) agent.SetDestination(patrolTarget);

        Vector3 distanceToTarget = transform.position - patrolTarget;

        if (distanceToTarget.magnitude < 3f) targetSet = false; //If Alan is close to the target find a new target 
    }

    private void SetPatrolPoint() //Note to self, use Random.Range + a sphere around the player to set new walk points
    {
        float randomZ = Random.Range(-patrolRadius, patrolRadius);
        float randomX = Random.Range(-patrolRadius, patrolRadius );

        patrolTarget = new Vector3 (playerRef.transform.position.x + randomX, playerRef.transform.position.y, playerRef.transform.position.z + randomZ);

        NavMeshHit hit;
        if (NavMesh.Raycast(patrolTarget, -transform.up, out hit, 3)) //If it is unable to hit a ground object it won't set walkpoint to true meaning the function is called until a walkpoint within the area is found
        {
          
            agent.SetDestination(patrolTarget);
            if (agent.path.status == NavMeshPathStatus.PathComplete)
            {
                targetSet = true;
            }
            }
        }

    // Update is called once per frame
    void Update()
    {
        if (aiActive == true && agro == false) 
        {
            Patrol();
            DrawPath(agent.path);
        }
        if (agro == true && aiActive == true)
        {
            agent.SetDestination(playerRef.transform.position);
            timeSinceSpotted -= Time.deltaTime;
            DrawPath(agent.path);

            if (timeSinceSpotted <= 0)
            {
                agro = false;
                timeSinceSpotted = agroTimer;
                targetSet = false;

            }
            
           
        }

    

 


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.UpdateGameState(GameState.LoseMenu);
        }
    }

    public void Spotted(bool x)
    {
        agro = x;
        timeSinceSpotted = agroTimer;


    }

    /*
   private void Arrived() Legacy code from the waypoint system.
    {
        lastPoint++;  ///

        if (lastPoint == spawnPoints.Length)
        {
            lastPoint = 0;
        };

        if (lastPoint == spawnPoints.Length - 1)
        {
            patrolTarget = spawnPoints[0];
        }
        else
        {
            patrolTarget = spawnPoints[lastPoint + 1];
        }
    }
    */

    private void DrawPath(NavMeshPath path)
    {
       
        if (path.corners.Length < 2)
        { //if the path has 1 or no corners, there is no need
            return;


        }
       
        /*line.SetVertexCount(path.corners.Length); //set the array of positions to the amount of corners
        for (int i = 1; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position
        }
        */
        RaycastHit hit;
        if (Physics.Linecast(path.corners[0], path.corners[1], out hit)) //Using the path we compare if there is an object in the way. 
        {
            float dist = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);
            if (hit.collider.gameObject.TryGetComponent(out IInteractable interactObj)&& dist <= 10)
            {
                interactObj.Interact(); //calls the interact of the object being looked at.
            }
            if(hit.transform.gameObject.layer == wall)
            {
                print("I am stuck");
                targetSet = false;
            }
        }


    }
}
