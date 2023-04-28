using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabMirrorHUDScript2: MonoBehaviour
{
    private PlayerScript playerScript;
    public Image grabUI;

    void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        grabUI = this.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (playerScript.LineForGrab())
        {
            grabUI.enabled = true;
        }
        else
        {
            grabUI.enabled = false;
        }
    }
}
