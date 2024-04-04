using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cart_Controller : MonoBehaviour
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
            
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        EnemySpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiActive == true) //(Works sort of)
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

        currentMoveTime = 0;
    }
    
   
}
