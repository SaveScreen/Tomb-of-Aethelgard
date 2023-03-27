using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneScript : MonoBehaviour
{
    public static bool cutscene;
    public bool startcutscene;
    public GameObject scene1;

    // Start is called before the first frame update
    private void Awake()
    {
        cutscene = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Starts cutscene
        if (startcutscene == true)
        {
            cutscene = true;
        }


    }

    private IEnumerator CutsceneTimer()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
