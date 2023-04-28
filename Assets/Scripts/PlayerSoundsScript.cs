using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsScript : MonoBehaviour
{

    [Header("Gameplay Sounds")]
    public AudioSource playerAudio;
    public AudioSource LandAudio;
    public AudioSource JumpAudio;
    public AudioClip jumpAudio;
    public AudioClip landAudio;
    public AudioClip runAudio;
    public AudioClip wallrunAudio;

    [Header("Random Audio Sounds")]
    public AudioSource randomAudioSource;
    public AudioClip[] randomAudioClipList;
    private float audioFrequencyLowBound = 10f;
    private float audioFrequencyHighBound = 30f;
    private float randomAudioTimer;
    
    public void Start(){
        randomAudioTimer = Time.time + Random.Range(audioFrequencyLowBound, audioFrequencyHighBound);
    }

    public void Update(){
        CheckRandomClip();
    }

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

    private void PlayRandomClip(){
        int clipIndex = Random.Range(0, randomAudioClipList.Length);
        randomAudioSource.clip = randomAudioClipList[clipIndex];
        randomAudioSource.Play();
    }

    private void CheckRandomClip(){
        if(Time.time >= randomAudioTimer){
            randomAudioTimer = Time.time + Random.Range(audioFrequencyLowBound, audioFrequencyHighBound);
            PlayRandomClip();
        }
        //yield return new WaitForSeconds(0.5f);
    }


  
}
