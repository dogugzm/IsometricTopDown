using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake instance { get; private set; }

    CinemachineVirtualCamera vCam;
    float ShakeTimer;

    private void Awake()
    {
        instance = this;
        vCam = GetComponent<CinemachineVirtualCamera>();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (ShakeTimer>0)
        {
            ShakeTimer -= Time.deltaTime;
            if (ShakeTimer<=0)
            {
                CinemachineBasicMultiChannelPerlin noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                noise.m_AmplitudeGain = 0;
            }
        }
    }

    public void ShakeCamera(float intensity,float time)
    {
        CinemachineBasicMultiChannelPerlin noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = intensity;
        ShakeTimer = time;

    }
}
