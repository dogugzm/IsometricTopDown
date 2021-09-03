
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeTest : MonoBehaviour
{
    public static VolumeTest instance;

    public Volume volume;
    ChromaticAberration ca;
    //private Vignette vg;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
        volume.profile.TryGet<ChromaticAberration>(out ca);
        //v.profile.TryGet(out vg);
    }

    
    public IEnumerator RollEffect()
    {
        ca.intensity.value = 1f;
        yield return new WaitForSeconds(1f);
        ca.intensity.value = 0f;

    }

}
