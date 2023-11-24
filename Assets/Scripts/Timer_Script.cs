using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text timerText;




    void Start()
    {
        currentTime = timeLimit; //These two lines could be placed within a bigger function for reseting the level possibly since respawning would do the same thing
        counting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (counting == true && currentTime > 0) //A method of displaying this is required since print won't work for the demo 
        {
            currentTime -= 1 * Time.deltaTime;
            print(currentTime.ToString("0"));
            timerText.text = currentTime.ToString("0");
        }
        if (counting == true && currentTime <= 0) { //This area allows for a check trigger to stop counting
            print("Times Up");
            counting = false;
        }
        if(counting == false)
        {
            //This is a place holder used for when we reset the player and change menus. Right now we are figuring that process out 
        }
    }
}
