using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alan_Mov : MonoBehaviour
{
    //The following section helps dictate where they can spawn and the points they will track to 
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private Transform[] trackingPoints; //Look into a way to insert the player into one of these points later
    private int lastPoint; //Tracks that last point in the array Alan visited. This section can cause problems later when player tracking is added so be careful. 
    private bool aiActive = false;
    private Transform activeTarget; //Where Alan is going

    private float currentMoveTime;
    [SerializeField]
    private float moveTime;

    ///The Following Section is for field of View 


    public float radius;
    [Range(0,360)]
    public float angle;

    [SerializeField]
    public GameObject playerOb;

    public LayerMask playerMask;
    public LayerMask obstacleMask;

    private bool spotted = false;


    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
        
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        
        if(state == GameState.GameStart)
        {
            EnemySpawn(); //This could be handled by the gamemanager in the future. When the gamemanager changes it would call Alan_Mov's Enemy Spawn void maybe instead but will work on that later

           
        }
        else{
            aiActive = false;
            StopCoroutine(PlayerDetection());
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aiActive == true) //(Works sort of)
        {
            currentMoveTime += Time.deltaTime;
            transform.position = Vector3.Lerp(trackingPoints[lastPoint].position,
             activeTarget.position,
             currentMoveTime / moveTime);
            
            if(transform.position == activeTarget.position)
            {
                Arrived();
               
            }
        }
        if(spotted == true) {
            print("I can see you");
        }
    }

    private void EnemySpawn()
    {
       int tempPoint = Random.Range (0, spawnPoints.Length-1); //Looks at the points that Alan has then selects one at random to spawn at.
        this.transform.position = spawnPoints[tempPoint].position;
      
        lastPoint = tempPoint;
        if(tempPoint == spawnPoints.Length - 1)
        {
            activeTarget = spawnPoints[0];
        }
        else
        {
            activeTarget = spawnPoints[tempPoint + 1];
        }
        aiActive = true;
        transform.LookAt(activeTarget); //This rotates the model to look where they are walking
        StartCoroutine(PlayerDetection());

    }

    private void Arrived(){ //Called when Alan reachs their target

        lastPoint++;  ///

        if(lastPoint == spawnPoints.Length)
        {
            lastPoint = 0; 
        }

        currentMoveTime = 0;

        if (lastPoint == spawnPoints.Length - 1)
        {
            activeTarget = spawnPoints[0];
        }
        else
        {
            activeTarget = spawnPoints[lastPoint + 1];
        }
        transform.LookAt(activeTarget); //This rotates the model to look where they are walking

    }

    private IEnumerator PlayerDetection()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        while (true)
        {
            yield return wait;
            PlayerCheck();
        }
    }

    private void PlayerCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, playerMask);

        if (rangeCheck.Length != 0)
        { //This is to make sure that if the player is within the radius it does further checks
            Transform target = rangeCheck[0].transform;
            Vector3 directionToPlayer = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToPlayer) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToTarget, playerMask))
                    spotted = true;
                else
                    spotted = false;

            }
            else
                spotted = false;
        }
        else if (spotted) 
            spotted = false;
    }


}
