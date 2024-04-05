using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject allenPrefab;
    private GameObject allen;
    // Start is called before the first frame update
    [SerializeField] private Transform[] spawnPoints;
    void Start()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

    }


    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        if (state == GameState.GameStart)
        {
            if (allen == null) Spawn();
        }
        else
        {

        }
        if (state == GameState.LoseMenu)
        {
            if (allen != null)
            {
                DestroyObject(allen);

            }
        }
    }

    private void Spawn()
    {
        if (allen == null)
        {
            print("Spawn is called");
            int tempPoint = Random.Range(0, spawnPoints.Length - 1);
            allen = Instantiate(allenPrefab, new Vector3(spawnPoints[tempPoint].transform.position.x, spawnPoints[tempPoint].transform.position.y, spawnPoints[tempPoint].transform.position.z), Quaternion.identity);
            allen.name = "Allen";
        }

        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Alan");
        if (gameObjects.Length > 1)
        {
            DestroyImmediate(gameObjects[1]);

        }
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
