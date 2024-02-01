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


    
    private Transform patrolTarget;

    [SerializeField]
    private LineRenderer line;
    private bool aiActive = false;

    [SerializeField]
    private Transform[] spawnPoints;

    private int lastPoint;




    /// <summary>
    /// This script is to test the navmesh, it will be incoperated into the Alan mov once the floor is completed.
    /// </summary>

    void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }
    private void GameManagerOnOnGameStateChanged(GameState state)
    {

        if (state == GameState.GameStart)
        {
            EnemySpawn(); //This could be handled by the gamemanager in the future. When the gamemanager changes it would call Alan_Mov's Enemy Spawn void maybe instead but will work on that later


        }
        else
        {
            aiActive = false;

        }
    }
    private void EnemySpawn()
    {
        int tempPoint = Random.Range(0, spawnPoints.Length - 1); //Looks at the points that Alan has then selects one at random to spawn at.
        this.transform.position = spawnPoints[tempPoint].position;

        lastPoint = tempPoint;
        if (tempPoint == spawnPoints.Length - 1)
        {
            patrolTarget = spawnPoints[0];
        }
        else
        {
            patrolTarget = spawnPoints[tempPoint + 1];
        }
        aiActive = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (aiActive == true && agro == false) //(Works sort of)
        {
            agent.SetDestination(patrolTarget.position);
            line.SetPosition(0, transform.position); //set the line's origin
            DrawPath(agent.path);
            if (agent)
            {
                if (transform.position.x == patrolTarget.position.x && transform.position.z == patrolTarget.position.z)
                {
                    Arrived();

                }
            }
        }
        if (agro == true && aiActive == true)
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
            
            if(transform.position.x == playerRef.transform.position.x && transform.position.z == playerRef.transform.position.z)
            {
                GameManager.Instance.UpdateGameState(GameState.LoseMenu);
            }
        }

    

 


    }

    public void Spotted(bool x)
    {
        agro = x;
        timeSinceSpotted = agroTimer;


    }

   private void Arrived()
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
        }

    }
}
