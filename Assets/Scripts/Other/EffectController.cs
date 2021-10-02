using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectController : MonoBehaviour
{
    public PostProcessProfile dashProfile;
    public PostProcessProfile normalProfile;
    PostProcessVolume PPVolume;


    private void Awake() {
        PPVolume = GetComponent<PostProcessVolume>();
    }
    
    public void DashEffectActivate()
    {
        PPVolume.profile = dashProfile;
    }
    public void NormalProfile()
    {
        PPVolume.profile = normalProfile;

    }
}
