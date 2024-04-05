using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerSpotted : MonoBehaviour
{
    [SerializeField] public Volume volume;
    [SerializeField] ChromaticAberration ca;
    public float intensity = 10f;

    [SerializeField] public AudioSource backgroundMusic;
    [SerializeField] public AudioSource heartBeat;
    [SerializeField] public AudioSource heart;

    GameState act;
    public float pitch = 1.0f;

    private void Start()
    {
        volume = GameObject.Find("PlayerCamera").GetComponent<Volume>();
        ChromaticAberration temporary;
        if (volume.profile.TryGet<ChromaticAberration>(out temporary))
        {
            ca = temporary;
        }
        backgroundMusic = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        heartBeat = GameObject.Find("HeartBeat").GetComponent<AudioSource>();
        heart = GameObject.Find("Heart").GetComponent<AudioSource>();

    }

    public void Seen()
    {
        StartCoroutine(StartEffect());
        makeBackGroundMusicGoFaster();
        makeHeartBeatSoundGoFaster();
        heart.enabled = true;
    }

    public void NotSeen()
    {
        StartCoroutine(StopEffect());
        makeHeartBeatSoundStop();
        makeBackgroundMusicNormal();
        heart.enabled = false;
    }
     void Update()
    {
        if(act != GameState.GameStart)
        {
            heart.enabled = false;
        }
    }
    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        if (state == GameState.GameStart)
        {
            
        }
        else
        {
            heart.enabled = false;
        }
    }

    public void ResetEffect()
    {
        StartCoroutine(NoEffect());
    }

    private IEnumerator StartEffect()
    {
        intensity = 10f;

        ca.intensity.value = intensity;

        yield return intensity;
    }

    private IEnumerator StopEffect()
    {
        while (intensity > 0)
        {
            intensity -= 0.01f;

            if (intensity < 0)
            {
                intensity = 0;
            }

            ca.intensity.value = intensity;
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }

    private IEnumerator NoEffect()
    {
        intensity = 0f;

        ca.intensity.value = intensity;

        yield return intensity;
    }

    private void makeHeartBeatSoundGoFaster()
    {
        //heartBeat.Play();
    }

    private void makeHeartBeatSoundStop()
    {
        //heartBeat.Pause();   
    }

    private void makeBackGroundMusicGoFaster()
    {
        backgroundMusic.pitch = Mathf.Lerp(pitch, pitch + 2.0f, 1.0f);
    }

    private void makeBackgroundMusicNormal()
    {
        backgroundMusic.pitch = 1.0f;
    }
}
