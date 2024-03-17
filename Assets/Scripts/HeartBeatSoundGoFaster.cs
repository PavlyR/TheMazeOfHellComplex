using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HeartBeatSoundGoFaster : Timer_Script
{
    [SerializeField] AudioSource HeartBeat;

    private float pitch;
    private float transition = 1.75f;
    private float percentage = 0f;

    public bool play;

    // Start is called before the first frame update
    void Start()
    {
        HeartBeating();
    }

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.GameStart)
        {
            play = true;
        }
        else
        {
            play = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HeartBeating()
    {
        if (play == true && Timer_Script.currentTime >= 60.0f)
        {
            HeartBeat.pitch = pitch;
        }
        if (play == true && Timer_Script.currentTime <= 60.0f)
        {
            HeartBeat.pitch = Mathf.Lerp(pitch, pitch * 2.0f, percentage);
            percentage += Time.deltaTime / transition;
        }
        if (play == true && Timer_Script.currentTime <= 30.0f)
        {
            HeartBeat.pitch = Mathf.Lerp(pitch, pitch + 0.5f, percentage);
            percentage += Time.deltaTime / transition;
        }
        if (play == true && Timer_Script.currentTime <= 10.0f)
        {
            HeartBeat.pitch = Mathf.Lerp(pitch, pitch + 0.5f, percentage);
            percentage += Time.deltaTime / transition;
        }
        if (play == false || Timer_Script.currentTime == 0f)
        {
            HeartBeat.Pause();
        }
    }
}
