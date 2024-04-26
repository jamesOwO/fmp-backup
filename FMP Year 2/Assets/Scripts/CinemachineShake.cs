using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance {get; private set;}

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance= this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void shakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain= intensity;
        shakeTimer= time;
    }
    // Update is called once per frame
    private void Update()
    {
        if (shakeTimer> 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer < 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>() ;
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
