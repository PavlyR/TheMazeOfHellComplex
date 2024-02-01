using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : GameManager
{
    AudioSource background;

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameState.GameStart)
        {
            SoundStart();
            Debug.Log("started");
        }
    }

    void SoundStart()
    {
        GameManager.Instance.UpdateGameState(GameState.GameStart);
        background.Play();
    }

}
