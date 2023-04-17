using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherooScript : MonoBehaviour
{
    private Scene currentscene;
    private GameObject player;

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

    void OnCollisionEnter(Collision other) {
        if (player != null) {
            if (currentscene.name == "TutorialLevel") {
                SceneManager.LoadScene("HubLevel");
            }
        }
    }
}
