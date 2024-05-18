using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ShakeController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    public float shakeDuration = 0.5f;
    public float shakeAmplitude = 1.2f;
    public float shakeFrequency = 2.0f;

    void Start()
    {
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    public void Shake()
    {
        StartCoroutine(ShakeElapseTime());
    }

    public IEnumerator ShakeElapseTime()
    {
        noise.m_AmplitudeGain = shakeAmplitude;
        noise.m_FrequencyGain = shakeFrequency;
        yield return new WaitForSecondsRealtime(shakeDuration);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }
}
