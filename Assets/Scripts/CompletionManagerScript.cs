using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionManagerScript : MonoBehaviour
{
    public static bool tutorialcomplete;
    public static bool level1complete;
    public static bool level2complete;
    public static bool level3complete;

    public static bool allclear;

    void Awake() {
        tutorialcomplete = false;
        level1complete = false;
        level2complete = false;
        level3complete = false;
        allclear = false;
    }

    void Update() {
        if (tutorialcomplete && level1complete && level2complete && level3complete) {
            allclear = true;
        }
    }
}
