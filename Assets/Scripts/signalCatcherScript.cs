using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signalCatcherScript : MonoBehaviour
{
    public int signalCatcherID;
    
    private bool hasBeenActivated = false; //think of this as a toggle/switch
    private bool isActivated = false; //think of this as a held/presed button

    public enum LightColor{
        any,
        red,
        blue,
        green
    }

    public LightColor catcherColor = 0;

    [Header("Audio")]
    public AudioSource isActivatedSoundPlayer;
    public AudioSource hasBeenActivatedSoundPlayer;

    public bool GetHasBeenActivated()
    {
        return hasBeenActivated;
    }
    public bool GetIsActivated()
    {
        return isActivated;
    }
    public void SetHasBeenActivated(bool b)
    {
        if(!hasBeenActivated && b){
            hasBeenActivatedSoundPlayer.Play();
        }
        hasBeenActivated = b;
    }
    public void SetIsActivated(bool b)
    {
        if(b){
            if(!isActivatedSoundPlayer.isPlaying){
                isActivatedSoundPlayer.Play();
            }
        } else{
            if(isActivatedSoundPlayer.isPlaying){
                isActivatedSoundPlayer.Stop();
            }
        }
        isActivated = b;
    }

    public int GetColor(){
        return (int)catcherColor;
    }



}
