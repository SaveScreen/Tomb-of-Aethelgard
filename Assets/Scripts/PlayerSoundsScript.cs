using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsScript : MonoBehaviour
{

    public AudioSource playerAudio;
    public AudioClip jumpAudio;
    public AudioClip landAudio;
    public AudioClip runAudio;
    public AudioClip wallrunAudio;
    
    
    public void PlayJumpSound(){
        playerAudio.PlayOneShot(jumpAudio);
    }
    public void PlayLandingSound(){
        playerAudio.PlayOneShot(landAudio);
    }
    public void PlayRunSound(){
        playerAudio.clip = runAudio;
        playerAudio.Play();
    }
    public void PlayWallrunningSound(){
        playerAudio.clip = wallrunAudio;
        playerAudio.Play();
    }
  
}
