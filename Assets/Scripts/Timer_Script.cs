using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor; //Allows you to use the textmesh timer

public class Timer_Script : MonoBehaviour


    /*
     * This script has some clear issues. Right now the player death/lose script will be called by this then the level will reset calling another script.
     * However this seems strange, we could try to break down each of these into functions that can be called in the gamemanager instead. Before doing this we should do a clear writeup
     * of how that would be done.
     */
{
    float currentTime = 0f;
    public float timeLimit;
    bool counting;
    //[SerializeField] private TextMeshProUGUI uiTimer;
    [SerializeField] private VisualTreeAsset Timer;

    void Start()
    {
        currentTime = timeLimit; //These two lines could be placed within a bigger function for reseting the level possibly since respawning would do the same thing
        counting = false;
    }


    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

    }

    private void GameManagerOnOnGameStateChanged(GameState state) //Whenever the player "spawns" in
    {
        if (state == GameState.GameStart)
        {
            counting = true;
            currentTime = timeLimit; //resets timer to default when this isn't the first run
        }
        else if (state == GameState.WinMenu)
        {
            counting = false;
            currentTime = timeLimit;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (counting == true && currentTime > 0) //A method of displaying this is required since print won't work for the demo 
        {
            currentTime -= 1 * Time.deltaTime;
            //timerText.text = currentTime.ToString("0");
            //uiTimer.text = currentTime.ToString("0");
            
            
        }
        if (counting == true && currentTime <= 0) { //This area allows for a check trigger to stop counting
            print("Times Up");
            counting = false;
            GameManager.Instance.UpdateGameState(GameState.LoseMenu);
        }
    }
}
