using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_Script : MonoBehaviour


    /*
     * This script has some clear issues 
     */
{
    float currentTime = 0f;
    public float timeLimit;
    bool counting;




    void Start()
    {
        currentTime = timeLimit;
        counting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (counting == true && timeLimit >= 0) 
        {
            currentTime -= 1 * Time.deltaTime;
            print(currentTime.ToString("0"));
        }
        if (counting == true && timeLimit <= 0) { //This area allows for a check trigger to stop counting
            print("Times Up");
            counting = false;
        }
        if(counting == false)
        {
            //This is a place holder used for when we reset the player and change menus. Right now we are figuring that process out 
        }
    }
}
