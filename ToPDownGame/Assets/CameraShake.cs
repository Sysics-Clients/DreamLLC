using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    public float timeToshake;
    float intialtime;
    // Start is called before the first frame update
    private void OnEnable()
    {
        GeneralEvents.takeDamege += Shake;
        cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void OnDisable()
    {
        GeneralEvents.takeDamege -= Shake;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (intialtime>0)
        {
            intialtime -= Time.deltaTime;

        }
        else
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        }
    }
    void Shake()
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 1;
        intialtime = timeToshake;

    }
}
