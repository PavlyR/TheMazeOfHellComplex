using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class SoundGoFaster : Timer_Script
{
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioClip music;

    [SerializeField] AudioSource heartBeatSound;
    [SerializeField] AudioClip beat;

    private float volume = 0.25f;
    private float pitch = 1.0f;
    private float transition = 1.75f;
    private float percentage = 0f;
    public bool play;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic.PlayOneShot(music);
        backgroundMusic.PlayScheduled(AudioSettings.dspTime + music.length);

        heartBeatSound.PlayOneShot(beat);
        heartBeatSound.PlayScheduled(AudioSettings.dspTime + music.length);
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
        if (play == true && Timer_Script.currentTime > 10.0f)
        {
            backgroundMusic.pitch = pitch;

            heartBeatSound.pitch = pitch;
            heartBeatSound.volume = volume;
        }
        if (play == true && Timer_Script.currentTime <= 60.0f)
        {
            backgroundMusic.pitch = Mathf.Lerp(pitch, 2.0f, percentage);

            heartBeatSound.volume = 0.7f;
            heartBeatSound.pitch = Mathf.Lerp(pitch, 1.5f, percentage);

            percentage += Time.deltaTime / transition;
        }
        if (play == true && Timer_Script.currentTime <= 30.0f)
        {
            backgroundMusic.pitch = Mathf.Lerp(2.0f, 2.5f, percentage);

            heartBeatSound.volume = 0.8f;
            heartBeatSound.pitch = Mathf.Lerp(pitch, 2.0f, percentage);

            percentage += Time.deltaTime / transition;
        }
        if (play == true && Timer_Script.currentTime <= 10.0f)
        {
            backgroundMusic.pitch = Mathf.Lerp(2.5f, 3.0f, percentage);

            heartBeatSound.volume = 1f;
            heartBeatSound.pitch = Mathf.Lerp(pitch, 2.5f, percentage);

            percentage += Time.deltaTime / transition;
        }
        if (play == false && Timer_Script.currentTime == 0f)
        {
            backgroundMusic.Stop();
            heartBeatSound.Stop();
        }
    }
}
