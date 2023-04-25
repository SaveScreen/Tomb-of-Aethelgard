using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsScript : MonoBehaviour
{

    public AudioSource playerAudio;
    public AudioSource LandAudio;
    public AudioSource JumpAudio;
    public AudioClip jumpAudio;
    public AudioClip landAudio;
    public AudioClip runAudio;
    public AudioClip wallrunAudio;
    
    
    public void PlayJumpSound(){
        JumpAudio.PlayOneShot(jumpAudio);
    }
    public void PlayLandingSound(){
        LandAudio.clip = landAudio;
        LandAudio.Play();
    }
    public void StopLandingSound() {
        LandAudio.Stop();
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
