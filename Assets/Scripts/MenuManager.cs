using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenu, loseMenu, playerUI;
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
        loseMenu.SetActive(state == GameState.LoseMenu);
        playerUI.SetActive(state == GameState.GameStart);
        /*
        if (state != GameState.StartMenu || state != GameState.LoseMenu)//When the game state changes if the state isn't the start state it will make sure to turn off that menu.
        {
            startMenu.SetActive(false);
            loseMenu.SetActive(false);
        }
        */
    }

    public void GameStart()
    {
        Debug.Log("The Game is starting");
        GameManager.Instance.UpdateGameState(GameState.GameStart);
    }
    public void MainMenuStart()
    {
        GameManager.Instance.UpdateGameState(GameState.StartMenu);
    }
    public void GameQuit()
    {
        Debug.Log("The Game is over");

        UnityEditor.EditorApplication.isPlaying = false; //This version works while in editor

        Application.Quit(); //This only works when it is exported into an application
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
