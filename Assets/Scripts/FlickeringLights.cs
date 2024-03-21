using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    public Light _light;

    public float MinTime;
    public float MaxTime;
    public float Timer;

    public AudioSource AS;
    public AudioClip LightAudio;

    void Start ()
    {
        _light = GetComponent<Light>();
        AS = GetComponent<AudioSource>();
        Timer = Random.Range(MinTime, MaxTime);
    }

    void Update ()
    {
        FlickerLight();
        
    }

    void FlickerLight()
    {
        if (Timer > 0)
             Timer -= Time.deltaTime;

        if(Timer<=0)
        {
         _light.enabled = !_light.enabled;
         Timer = Random.Range(MinTime, MaxTime);
         AS.PlayOneShot(LightAudio);
        }
    }

}
