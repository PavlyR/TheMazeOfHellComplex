using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public GameObject footstep;

    // Start is called before the first frame update
    void Start()
    {
        footstep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w") || Input.GetKey("up"))
        {
            FootSteps();
        }
        if(Input.GetKey("a") || Input.GetKey("left"))
        {
            FootSteps();
        }
        if(Input.GetKey("s") || Input.GetKey("down"))
        {
            FootSteps();
        }
        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            FootSteps();
        }

        if (Input.GetKeyUp("w") || Input.GetKeyUp("up"))
        {
            StopFootSteps();
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("left"))
        {
            StopFootSteps();
        }
        if (Input.GetKeyUp("s") || Input.GetKeyUp("down"))
        {
            StopFootSteps();
        }
        if (Input.GetKeyUp("d") || Input.GetKeyUp("right"))
        {
            StopFootSteps();
        }
    }

    void FootSteps()
    {
        footstep.SetActive(true);
    }

    void StopFootSteps()
    {
        footstep.SetActive(false);
    }
}
