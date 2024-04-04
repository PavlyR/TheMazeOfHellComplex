using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerSpotted : MonoBehaviour
{
    [SerializeField] public Volume volume;
    [SerializeField] Vignette vignette;
    [SerializeField] public float intensity = 0f;

    private void Start()
    {
        volume = GameObject.Find("PlayerCamera").GetComponent<Volume>();
        Vignette temporary;
        if (volume.profile.TryGet<Vignette>(out temporary))
        {
            vignette = temporary;
        }
    }

    public void Seen()
    {
        print("I think I have been spotted");
        StartCoroutine(StartEffect());
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
        intensity = 0.5f;

        

        yield return intensity;
    }

}
