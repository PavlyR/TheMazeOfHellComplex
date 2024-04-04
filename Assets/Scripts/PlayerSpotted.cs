using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerSpotted : MonoBehaviour
{
    [SerializeField] public PostProcessVolume volume;
    [SerializeField] public Vignette vignette;
    [SerializeField] public float intensity = 0f;

    private void Start()
    {
        volume = GameObject.Find("PostFX").GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<Vignette>(out vignette);
    }

    public void Seen()
    {
        print("I think I have been spotted");
        StartCoroutine(StartEffect());
    }

    private IEnumerator StartEffect()
    {
        intensity = 0.5f;

        vignette.enabled.Override(true);
        vignette.intensity.Override(intensity);

        yield return intensity;
    }

}
