using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherooScript : MonoBehaviour
{
    private Scene currentscene;
    private GameObject player;
    
    //SET IN INSPECTOR
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        currentscene = SceneManager.GetActiveScene();
        player = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        
        if (other.gameObject == player) {
            
            GameSettingsScript.settingschanged = true;
            if (currentscene.name == "TutorialLevelConcept") {
                SceneManager.LoadScene("HubLevel");
                CompletionManagerScript.tutorialcomplete = true;
            }

            if (currentscene.name == "HubLevel" && level == 1)
            {
                SceneManager.LoadScene("VSLevelScene");
            }
            if (currentscene.name == "HubLevel" && level == 2)
            {
                SceneManager.LoadScene("Level2Scene");
            }
            if (currentscene.name == "HubLevel" && level == 3) {
                SceneManager.LoadScene("Level3Scene");
            }
            if (currentscene.name == "HubLevel" && level == 4)
            {
                SceneManager.LoadScene("EndCutsceneScene");
            }


            //For exiting the other levels
            if (currentscene.name == "VSLevelScene" && level == 1) {
                SceneManager.LoadScene("HubLevel");
                CompletionManagerScript.level1complete = true;
            }
            if (currentscene.name == "Level2Scene" && level == 2)
            {
                SceneManager.LoadScene("HubLevel");
                CompletionManagerScript.level2complete = true;
                
            }
            if (currentscene.name == "Level3Scene" && level == 3)
            {
                SceneManager.LoadScene("HubLevel");
                CompletionManagerScript.level3complete = true;
            }
            
        }
    }
}
