using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameSettingsScript : MonoBehaviour
{
    public static float mousexsensitivity;
    public static float mouseysensitivity;
    public static bool settingschanged = false;
    
    private GameObject cam;
    private CinemachineFreeLook freelookcam;
    private Scene currentscene;
    private bool camerareferenced = false;
    


    // Start is called before the first frame update
    void Awake()
    {
        currentscene = SceneManager.GetActiveScene();
        if (currentscene.name != "MainMenuScene" && currentscene.name != "CutsceneScene" && currentscene.name != "EndCutsceneScene") {
            Debug.Log ("Found FreeLook");
            cam = GameObject.FindWithTag("FreeLook");
            freelookcam = cam.GetComponent<CinemachineFreeLook>();
            UpdateSensitivity();
            camerareferenced = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((settingschanged && camerareferenced)) {
            UpdateSensitivity();
        }
    }

    void UpdateSensitivity() {
        float sensitivityvalx = PlayerPrefs.GetFloat("SensitivityX");
        float sensitivityvaly = PlayerPrefs.GetFloat("SensitivityY");
        mousexsensitivity = sensitivityvalx;
        mouseysensitivity = sensitivityvaly;
        freelookcam.m_XAxis.m_MaxSpeed = mousexsensitivity;
        freelookcam.m_YAxis.m_MaxSpeed = mouseysensitivity;
        Debug.Log("Settings Changed to" + freelookcam.m_XAxis.m_MaxSpeed.ToString());
        Debug.Log("Settings Changed to" + freelookcam.m_YAxis.m_MaxSpeed.ToString());
        settingschanged = false;
    }
}
