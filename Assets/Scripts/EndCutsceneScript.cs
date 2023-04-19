using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndCutsceneScript : MonoBehaviour
{
    public TextMeshProUGUI textcomponent;
    public CanvasGroup transition;
    public float transitionspeed;
    private bool transitioning;
    private bool fadein;
    private bool endscene;
    public string[] lines;
    public InputAction next;
    private bool nextclicked;
    public float textspeed;
    private int index; //Line index
    private int panelindex; //Panel index
    public static bool cutscene;
    private bool cutscenecompleted;
    private bool dialoguestarted;
    public GameObject[] panels;
    

    private Scene currentscene;

    // Start is called before the first frame update
    private void Start()
    {
        cutscene = true;
        textcomponent.text = string.Empty;
        dialoguestarted = false;
        cutscenecompleted = false;
        currentscene = SceneManager.GetActiveScene();
        transitioning = false;
        fadein = true;
        transition.alpha = 0;
        foreach (GameObject p in panels)
        {
            p.SetActive(false);
        }
    }

    void OnEnable()
    {
        next.Enable();
    }

    void OnDisable()
    {
        next.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (cutscene == true)
        {

            nextclicked = next.WasPressedThisFrame();
            if (dialoguestarted == false)
            {

                StartDialogue();
            }

            if (nextclicked)
            {
                if (textcomponent.text == lines[index])
                {
                    //Panel changes will be on lines 2 and line 3
                    if (index == 0 || index == 3)
                    {
                        transitioning = true;
                    }
                    else
                    {
                        NextPage();
                    }


                }
                else
                {
                    StopAllCoroutines();
                    textcomponent.text = lines[index];
                }
            }

            if (transitioning == true)
            {
                PanelTransition();
            }
            if (endscene == true)
            {
                PanelTransition();
            }

        }

        if (cutscene == false)
        {
            dialoguestarted = false;
            textcomponent.text = string.Empty;
            if (currentscene.name == "EndCutsceneScene")
            {
                if (cutscenecompleted)
                {
                    StartCoroutine(ReturnToMenu());
                    
                }
            }
        }



    }

    void StartDialogue()
    {
        index = 0;
        panelindex = 0;
        panels[panelindex].SetActive(true);
        StartCoroutine(TypeOutCharacters());
        dialoguestarted = true;
    }

    void NextPage()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textcomponent.text = string.Empty;
            StartCoroutine(TypeOutCharacters());
        }
        else
        {
            endscene = true;

        }
    }

    void PanelTransition()
    {
        if (endscene == false)
        {
            if (fadein == true)
            {
                transition.alpha += transitionspeed * Time.deltaTime;
                if (transition.alpha >= 1)
                {
                    fadein = false;
                    textcomponent.text = string.Empty;
                    panelindex += 1;
                    NextPage();
                    panels[panelindex].SetActive(true);
                }
            }
            else
            {
                transition.alpha -= transitionspeed * Time.deltaTime;
                if (transition.alpha <= 0)
                {
                    transitioning = false;
                    fadein = true;
                }
            }
        }
        else
        {
            if (fadein == true)
            {
                transition.alpha += transitionspeed * Time.deltaTime;
                if (transition.alpha >= 1)
                {
                    cutscene = false;
                    cutscenecompleted = true;
                }
            }
        }



    }


    IEnumerator TypeOutCharacters()
    {
        //this types out each character one by one
        foreach (char c in lines[index].ToCharArray())
        {
            textcomponent.text += c;
            yield return new WaitForSeconds(textspeed);
        }

    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MainMenuScene");
    }
}
