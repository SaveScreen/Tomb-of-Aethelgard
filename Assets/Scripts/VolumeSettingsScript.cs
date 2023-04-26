using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingsScript : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; 
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", 0.75f);
            Load();
        }else{
            Load();
        }
    }

    public void ChangeVolume(){
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void Save(){
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }


    
}
