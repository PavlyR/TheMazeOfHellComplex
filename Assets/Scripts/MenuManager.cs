using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenu, loseMenu;
    // Start is called before the first frame update
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
        startMenu.SetActive(state == GameState.StartMenu); //By default the menu is set to intactive. Once gamemanager tells it that the Start menu is active it is visible to the player
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
