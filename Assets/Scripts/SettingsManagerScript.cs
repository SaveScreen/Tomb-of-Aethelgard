using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManagerScript : MonoBehaviour
{
    public Slider sliderx;
    public Slider slidery;
    public TextMeshProUGUI slidertextx;
    public TextMeshProUGUI slidertexty;
    public GameObject settingssaved;
    

    // Start is called before the first frame update
    void Start()
    {
        slidertextx.text = GameSettingsScript.mousexsensitivity.ToString();
        slidertexty.text = GameSettingsScript.mouseysensitivity.ToString();
        sliderx.value = GameSettingsScript.mousexsensitivity;
        slidery.value = GameSettingsScript.mouseysensitivity;
        settingssaved.SetActive(false);
        SaveSensitivity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSensitivityX(float sensitivity) {
        slidertextx.text = sensitivity.ToString("0.0");
        if (sliderx.value == sliderx.maxValue) {
            slidertextx.text = "LUDICROUS SPEED!";
            slidertextx.fontSize = 20f;
        }
        else {
            slidertextx.fontSize = 36f;
        }
        
    }

    public void ChangeSensitivityY(float sensitivity) {
        slidertexty.text = sensitivity.ToString("0.0");
        if (slidery.value == slidery.maxValue) {
            slidertexty.text = "LUDICROUS SPEED!";
            slidertexty.fontSize = 20f;
        } else {
            slidertexty.fontSize = 36f;
        }
    }

    public void SaveSensitivity() {
        float sensitivityvalx = sliderx.value;
        float sensitivityvaly = slidery.value;
        PlayerPrefs.SetFloat("SensitivityX",sensitivityvalx);
        PlayerPrefs.SetFloat("SensitivityY",sensitivityvaly);
        GameSettingsScript.mousexsensitivity = sensitivityvalx;
        GameSettingsScript.mouseysensitivity = sensitivityvaly;
        GameSettingsScript.settingschanged = true;
        settingssaved.SetActive(true);
        StartCoroutine(Waiting());
        Debug.Log("Settings Saved");
        LoadSensitivity();
    }

    public void LoadSensitivity() {
        float sensitivityvalx = PlayerPrefs.GetFloat("SensitivityX");
        float sensitivityvaly = PlayerPrefs.GetFloat("SensitivityY");
        GameSettingsScript.mousexsensitivity = sensitivityvalx;
        GameSettingsScript.mouseysensitivity = sensitivityvaly;
        sliderx.value = sensitivityvalx;
        slidery.value = sensitivityvaly;
    }

    IEnumerator Waiting() {
        yield return new WaitForSeconds(3.0f);
        settingssaved.SetActive(false);
        StopAllCoroutines();
    }
}
