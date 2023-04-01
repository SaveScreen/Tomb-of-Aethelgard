using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShakeManager : MonoBehaviour
{
    public static ScreenShakeManager instance;

    [SerializeField] private float GlobalShakeForce = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CameraShake(CinemachineImpulseSource impulseSoruce)
    {
        impulseSoruce.GenerateImpulseWithForce(GlobalShakeForce);
    }
}
