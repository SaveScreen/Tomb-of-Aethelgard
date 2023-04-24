using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabMirrorHUDScript : MonoBehaviour
{
    private PlayerScript playerScript;
    private Image grabMirrorHUD;

    void Start(){
        playerScript = FindObjectOfType<PlayerScript>();
        grabMirrorHUD = this.gameObject.GetComponent<Image>();
    }

    void Update(){
        if(playerScript.LineForRotate()){
            grabMirrorHUD.enabled = true;
            Debug.Log("On");
        }else{
            grabMirrorHUD.enabled = false;
            Debug.Log("Off");
        }
    }
}
