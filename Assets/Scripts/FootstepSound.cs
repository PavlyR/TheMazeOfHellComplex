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
        if(Input.GetKey("w"))
        {
            FootSteps();
        }
        if(Input.GetKey("a"))
        {
            FootSteps();
        }
        if(Input.GetKey("s"))
        {
            FootSteps();
        }
        if(Input.GetKey("d"))
        {
            FootSteps();
        }

        if (Input.GetKeyUp("w"))
        {
            StopFootSteps();
        }
        if (Input.GetKeyUp("a"))
        {
            StopFootSteps();
        }
        if (Input.GetKeyUp("s"))
        {
            StopFootSteps();
        }
        if (Input.GetKeyUp("d"))
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
