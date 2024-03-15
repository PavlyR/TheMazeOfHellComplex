using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMusic : PauseMenu
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource beat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused)
        {
            music.Pause();
            beat.Pause();
        }
        if (!PauseMenu.isPaused)
        {
            music.Play();
            beat.Play();
        }
    }
}
