using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCamera : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    private float _shakeTime;

    private static ShakeCamera _instance;
    private CinemachineBasicMultiChannelPerlin _noise;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        _noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public static void Shake(float amplitude, float frequency, float time)
    {
        _instance._noise.m_AmplitudeGain = amplitude;
        _instance._noise.m_FrequencyGain = frequency;
        _instance._shakeTime = time;
    }

    private void Update()
    {
        if (_shakeTime > 0)
        {
            _shakeTime -= Time.deltaTime;
            if (_shakeTime <= 0)
            {
                _noise.m_AmplitudeGain = 0;
                _noise.m_FrequencyGain = 0;
                _shakeTime = 0;
            }
        }
    }
}
