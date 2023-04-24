using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraRecenterScript : MonoBehaviour
{
    private CinemachineFreeLook cam;
    private bool pressedbutton;
    private bool recentering;
    public InputAction recenter;

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<CinemachineFreeLook>();
    }

    void OnEnable() {
        recenter.Enable();
    }

    void OnDisable() {
        recenter.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        pressedbutton = recenter.IsPressed();

        if (pressedbutton) {
            cam.m_RecenterToTargetHeading.m_enabled = true;

        }
        else {
            cam.m_RecenterToTargetHeading.m_enabled = false;
        }
    }
}
