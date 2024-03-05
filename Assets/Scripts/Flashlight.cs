using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light flashlight;

    public Transform orientation;

    // Start is called before the first frame update
    void Start()
    {
        flashlight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        orientation.rotation = flashlight.transform.rotation;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (flashlight != null)
            {
                flashlight.enabled = !flashlight.enabled;
            }
            else
            {
                Debug.Log("No");
            }
        }
    }
}
