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
    // Start is called before the first frame update
    void Start()
    {
        currentTime = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        print(currentTime.ToString("0"));
    }
}
