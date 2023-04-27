using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManagerScript : MonoBehaviour
{
    public Slider sliderx;
    public Slider slidery;
    public Toggle invertx;
    public Toggle inverty;
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
        invertx.isOn = GameSettingsScript.mousexinverted;
        inverty.isOn = GameSettingsScript.mouseyinverted;
        settingssaved.SetActive(false);
        SaveSettings();
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

    int boolToInt(bool val) {
        if (val)
        return 1;
        else
        return 0;
    }

    bool intToBool(int val) {
        if (val != 0) {
            return true;
        } else {
            return false;
        }
    }

    public void SaveSettings() {
        float sensitivityvalx = sliderx.value;
        float sensitivityvaly = slidery.value;
        int invertxval = boolToInt(invertx.isOn);
        int invertyval = boolToInt(inverty.isOn);
        PlayerPrefs.SetFloat("SensitivityX",sensitivityvalx);
        PlayerPrefs.SetFloat("SensitivityY",sensitivityvaly);
        PlayerPrefs.SetInt("InvertX", invertxval);
        PlayerPrefs.SetInt("InvertY", invertyval);
        
        GameSettingsScript.mousexsensitivity = sensitivityvalx;
        GameSettingsScript.mouseysensitivity = sensitivityvaly;
        GameSettingsScript.mousexinverted = intToBool(invertxval);
        GameSettingsScript.mouseyinverted = intToBool(invertyval);
        GameSettingsScript.settingschanged = true;

        settingssaved.SetActive(true);
        StartCoroutine(Waiting());
        Debug.Log("Settings Saved");
        LoadSettings();
    }

    public void LoadSettings() {
        float sensitivityvalx = PlayerPrefs.GetFloat("SensitivityX");
        float sensitivityvaly = PlayerPrefs.GetFloat("SensitivityY");
        int invertxval = PlayerPrefs.GetInt("InvertX");
        int invertyval = PlayerPrefs.GetInt("InvertY");

        GameSettingsScript.mousexsensitivity = sensitivityvalx;
        GameSettingsScript.mouseysensitivity = sensitivityvaly;
        GameSettingsScript.mousexinverted = intToBool(invertxval);
        GameSettingsScript.mouseyinverted = intToBool(invertyval);

        sliderx.value = sensitivityvalx;
        slidery.value = sensitivityvaly;
        invertx.isOn = intToBool(invertxval);
        inverty.isOn = intToBool(invertyval);
    }

    IEnumerator Waiting() {
        yield return new WaitForSeconds(3.0f);
        settingssaved.SetActive(false);
        StopAllCoroutines();
    }
}
