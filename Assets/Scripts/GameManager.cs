using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //Using system allows us to use "event Action"

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this; //Set t
    }
    void Start()
    {
        UpdateGameState(GameState.StartMenu);
    }

 
    void Update()
    {
        
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState) //This switch state looks at the state of the game and does something based on that.
        {
            case GameState.StartMenu:
                break;
            case GameState.GameStart:
                break;
            case GameState.LoseMenu:
                break;
            case GameState.VictoryMenu:
                break;
            default:
                break;

        }
        OnGameStateChanged?.Invoke(newState);//This checks to see if any scripts care about the event then will send them the info if they do
    }
}

public enum GameState
{
    StartMenu,
    GameStart,
    LoseMenu,
    VictoryMenu

};
