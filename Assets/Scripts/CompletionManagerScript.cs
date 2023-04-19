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
        if (tutorialcomplete && level1complete && level2complete && level3complete) {
            allclear = true;
        }
    }
}
