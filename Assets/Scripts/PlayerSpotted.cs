using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerSpotted : MonoBehaviour
{
    [SerializeField] public Volume volume;
    [SerializeField] ChromaticAberration ca;
    [SerializeField] public float intensity = 0f;

    [SerializeField] public GameObject backgroundMusicObject;
    [SerializeField] public AudioSource backgroundMusic;
    [SerializeField] public GameObject HeartBeatObject;
    [SerializeField] public AudioSource heartBeat;

    public float pitch = 1.0f;
    public float transition = 1.75f;
    public float percentage = 0f;

    private void Start()
    {
        volume = GameObject.Find("PlayerCamera").GetComponent<Volume>();
        ChromaticAberration temporary;
        if (volume.profile.TryGet<ChromaticAberration>(out temporary))
        {
            ca = temporary;
        }
        backgroundMusic = backgroundMusicObject.GetComponent<AudioSource>();
        heartBeat = HeartBeatObject.GetComponent<AudioSource>();
    }

    public void Seen()
    {
        StartCoroutine(StartEffect());
    }

    public void NotSeen()
    {
        StartCoroutine(StopEffect());
    }

    public void ResetEffect()
    {
        StartCoroutine(NoEffect());
    }

    private void Update()
    {
        if (volume == null) //Check to see if the Volume is assigned in this scene and if not finds the scene's Volume settings
        {
            volume = GameObject.FindGameObjectWithTag("FX").GetComponent<Volume>();
        }
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

}
