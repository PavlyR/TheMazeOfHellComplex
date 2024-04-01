using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ShadowZoneEffect : MonoBehaviour
{
    public float intensity = 0f;

    PostProcessVolume volume;
    Vignette vignette;

    public GameObject player;
    public Collider playerCollider;

    bool enter;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<Vignette>(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
       if (enter)
       {
            StartCoroutine(StartEffect());
       }
    }

    private IEnumerator StartEffect()
    {
        intensity = 0.4f;

        vignette.enabled.Override(true);
        vignette.intensity.Override(0.4f);

        yield return new WaitForSeconds(0.4f);

        while (intensity > 0)
        {
            intensity -= 0.01f;

            if (intensity < 0)
            {
                intensity = 0;
            }

            vignette.intensity.Override(intensity);

            yield return new WaitForSeconds(0.1f);
        }

        vignette.enabled.Override(false);
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(playerCollider.CompareTag("ShadowZone"))
        {
            enter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(playerCollider.CompareTag("ShadowZone"))
        {
            enter = false;
        }
    }
}
