using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherooScript : MonoBehaviour
{
    private Scene currentscene;
    private GameObject player;
    public static int levelscompleted;

    // Start is called before the first frame update
    void Start()
    {
        currentscene = SceneManager.GetActiveScene();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentscene.name == "TutorialLevel")
        {
            levelscompleted = 0;
        }
        
    }

    void OnCollisionEnter(Collision other) {
        if (player != null) {
            if (currentscene.name == "TutorialLevel") {
                SceneManager.LoadScene("HubLevel");
                levelscompleted = 1;
            }

            if (currentscene.name == "HubLevel" && levelscompleted == 1)
            {
                SceneManager.LoadScene("Level2Scene");
            }
            if (currentscene.name == "HubLevel" && levelscompleted == 2)
            {
                SceneManager.LoadScene("Level3Scene");
            }
            //Add code to trigger end cutscene here

            if (currentscene.name == "Level2Scene")
            {
                SceneManager.LoadScene("HubLevel");
                levelscompleted = 2;
            }
            if (currentscene.name == "Level3Scene")
            {
                SceneManager.LoadScene("HubLevel");
                levelscompleted = 3;
            }
        }
    }
}
