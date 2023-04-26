using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameSettingsScript : MonoBehaviour
{
    public static float mousexsensitivity = 120f;
    public static float mouseysensitivity = 1.5f;
   
    public static bool settingschanged = false;
    public static bool scenechanged = false;
    
    private GameObject cam;
    private CinemachineFreeLook freelookcam;
    private Scene currentscene;
    private bool camerareferenced = false;
    private float sensitivityvalx;
    private float sensitivityvaly;
    //private bool dontloadsensitivity = false;
    


    void Awake()
    {
        currentscene = SceneManager.GetActiveScene();
        if (currentscene.name != "MainMenuScene" && currentscene.name != "CutsceneScene" && currentscene.name != "EndCutsceneScene") {
            Debug.Log ("Found FreeLook");
            cam = GameObject.FindWithTag("FreeLook");
            freelookcam = cam.GetComponent<CinemachineFreeLook>();
            camerareferenced = true; 
        }
    
        sensitivityvalx = PlayerPrefs.GetFloat("SensitivityX");    
        sensitivityvaly = PlayerPrefs.GetFloat("SensitivityY"); 
        
    }

    void Start() {
        LoadOriginalSensitivity(); 
    }

    // Update is called once per frame
    void Update()
    {
        //If the settings are changed in the middle of a scene with a freelook.
        if ((settingschanged || scenechanged) && camerareferenced) {
            UpdateSensitivity();
            
        }
    }

    void LoadOriginalSensitivity() {
        freelookcam.m_XAxis.m_MaxSpeed = mousexsensitivity;
        freelookcam.m_YAxis.m_MaxSpeed = mouseysensitivity;
        Debug.Log("Settings Changed to" + freelookcam.m_XAxis.m_MaxSpeed.ToString());
        Debug.Log("Settings Changed to" + freelookcam.m_YAxis.m_MaxSpeed.ToString());
        //dontloadsensitivity = true;
    }

    void UpdateSensitivity() {
        freelookcam.m_XAxis.m_MaxSpeed = sensitivityvalx;
        freelookcam.m_YAxis.m_MaxSpeed = sensitivityvaly;
        mousexsensitivity = sensitivityvalx;
        mouseysensitivity = sensitivityvaly;
        Debug.Log("Settings Changed to" + freelookcam.m_XAxis.m_MaxSpeed.ToString());
        Debug.Log("Settings Changed to" + freelookcam.m_YAxis.m_MaxSpeed.ToString()); 
        settingschanged = false;
        scenechanged = false;
    }

    
}
