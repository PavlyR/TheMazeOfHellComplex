using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMusic : PauseMenu
{
    [SerializeField] AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused)
        {
            sound.Pause();
        }
        if (!PauseMenu.isPaused)
        {
            sound.Play();
        }
    }
}
