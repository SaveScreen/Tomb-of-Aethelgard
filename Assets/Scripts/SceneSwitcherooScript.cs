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
            Debug.Log("Ayyy");
            
            if (currentscene.name == "TutorialLevel") {
                SceneManager.LoadScene("HubLevel");
                
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
            //Add code to trigger end cutscene here


            //For exiting the other levels
            if (currentscene.name == "VSLevelScene") {
                SceneManager.LoadScene("HubLevel");
            }
            if (currentscene.name == "Level2Scene")
            {
                SceneManager.LoadScene("HubLevel");
                
            }
            if (currentscene.name == "Level3Scene")
            {
                SceneManager.LoadScene("HubLevel");
                
            }
            
        }
    }
}
