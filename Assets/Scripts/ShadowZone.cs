using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowZone : MonoBehaviour
{
    public bool enter = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enter = true;
            Debug.Log("Entered Shadow Zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enter = false;
            Debug.Log("Exited Shadow Zone");
        }
    }
}
