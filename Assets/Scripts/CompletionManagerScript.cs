using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionManagerScript : MonoBehaviour
{
    public static bool tutorialcomplete = false;
    public static bool level1complete = false;
    public static bool level2complete = false;
    public static bool level3complete = false;

    public static bool allclear = false;

    void Update() {
        if (level1complete && level2complete && level3complete) {
            allclear = true;
            //tutorial not needed to be cleared
        }

        //Debug
        if (Input.GetKeyDown(KeyCode.T)) {
            Debug.Log(tutorialcomplete.ToString());
            Debug.Log(level1complete.ToString());
            Debug.Log(level2complete.ToString());
            Debug.Log(level3complete.ToString());
            Debug.Log(allclear.ToString());
        }
        
    }
}
