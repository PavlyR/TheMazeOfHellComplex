using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class SoundGoFaster : Timer_Script
{
    [SerializeField] AudioSource background;
    [SerializeField] AudioClip sound;

    private float pitch = 1.0f;
    private float transition = 1.75f;
    private float percentage = 0f;

    public bool play;

    // Start is called before the first frame update
    void Start()
    {
        background.PlayOneShot(sound);
        background.PlayScheduled(AudioSettings.dspTime + sound.length);
        background.loop = true;
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
        if (play == true && Timer_Script.currentTime >= 60.0f)
        {
            background.pitch = pitch;
        }
        if (play == true && Timer_Script.currentTime <= 60.0f)
        {
            background.pitch = Mathf.Lerp(pitch, pitch * 2.0f, percentage);
            percentage += Time.deltaTime / transition;
        }
        if (play == true && Timer_Script.currentTime <= 30.0f)
        {
            background.pitch = Mathf.Lerp(pitch, 2.5f, percentage);
            percentage += Time.deltaTime / transition;
        }
        if (play == true && Timer_Script.currentTime <= 10.0f)
        {
            background.pitch = Mathf.Lerp(pitch, 3.0f, percentage);
            percentage += Time.deltaTime / transition;
        }
        if (play == false || Timer_Script.currentTime == 0f)
        {
            background.Stop();
        }
    }
}
