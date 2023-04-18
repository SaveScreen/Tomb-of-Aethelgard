using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CutsceneScript : MonoBehaviour
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
    private int index;
    private int panelindex;
    public static bool cutscene;
    private bool cutscenecompleted;
    private bool dialoguestarted;
    public GameObject[] panels;
    public GameObject loading;

    private Scene currentscene;

    private bool musicoff;
    public AudioSource music;
    public GameObject flameplay;
    private AudioSource flameplayer;

    // Start is called before the first frame update
    private void Start()
    {
        cutscene = true;
        loading.SetActive(false);
        textcomponent.text = string.Empty;
        dialoguestarted = false;
        cutscenecompleted = false;
        currentscene = SceneManager.GetActiveScene();
        music = gameObject.GetComponent<AudioSource>();
        flameplayer = flameplay.GetComponent<AudioSource>();
        music.Stop();
        flameplayer.Stop();
        transitioning = false;
        fadein = true;
        musicoff = true;
        transition.alpha = 0;
        foreach (GameObject p in panels) {
            p.SetActive(false);
        }
    }

    void OnEnable() {
        next.Enable();
    }

    void OnDisable() {
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
                    if (index == 2 || index == 3 || index == 4 || index == 5) {
                        transitioning = true;
                    }
                    else {
                        NextPage();
                    }

                        
                }
                else
                {
                    StopAllCoroutines();
                    textcomponent.text = lines[index];
                }
            }

            if (transitioning == true) {
                PanelTransition();
            }
            if (endscene == true) {
                PanelTransition();
            }

            if (panelindex == 1) {
                if (musicoff == true) {
                    flameplayer.loop = true;
                    music.volume = 0.3f;
                    music.loop = true;
                    flameplayer.volume = 0.2f;
                    flameplayer.Play();
                    music.Play();
                    musicoff = false;
                }
            }
            if (panelindex == 2) {
                if (musicoff == true) {
                    flameplayer.volume = 0.07f;
                    musicoff = false;
                }
            }
        }

        if (cutscene == false)
        {
            dialoguestarted = false;
            textcomponent.text = string.Empty;
            if (currentscene.name == "CutsceneScene") {
                if (cutscenecompleted) {
                    
                    SceneManager.LoadScene("TutorialLevelConcept");
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

    void PanelTransition() {
        if (endscene == false) {
            if (fadein == true) {
                transition.alpha += transitionspeed * Time.deltaTime;
                if (transition.alpha >= 1) {
                    fadein = false;
                    textcomponent.text = string.Empty;
                    panelindex += 1;
                    musicoff = true;
                    NextPage();
                    panels[panelindex].SetActive(true);
                }
            }
            else {
                transition.alpha -= transitionspeed * Time.deltaTime;
                if (transition.alpha <= 0) {
                    transitioning = false;
                    fadein = true;
                }
            }
        } else {
            if (fadein == true) {
                transition.alpha += transitionspeed * Time.deltaTime;
                if (transition.alpha >= 1) {
                    loading.SetActive(true);
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
}
