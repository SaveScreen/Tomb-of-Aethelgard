using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneScript : MonoBehaviour
{
    public TextMeshProUGUI textcomponent;
    public string[] lines;
    public float textspeed;
    private int index;
    public bool cutscene;
    private bool dialoguestarted;
    public GameObject scene1;

    // Start is called before the first frame update
    private void Start()
    {
        cutscene = false;
        scene1.SetActive(false);
        textcomponent.text = string.Empty;
        dialoguestarted = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (cutscene == true)
        {
        
            if (dialoguestarted == false)
            {
                scene1.SetActive(true);
                StartDialogue();
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (textcomponent.text == lines[index])
                {
                    NextPage();
                }
                else
                {
                    StopAllCoroutines();
                    textcomponent.text = lines[index];
                }
            }
        }

        if (cutscene == false)
        {
            scene1.SetActive(false);
            dialoguestarted = false;
            textcomponent.text = string.Empty;

            //Starts cutscene
            if (Input.GetKeyDown(KeyCode.R))
            {
                cutscene = true;
            }
        }

        

    }

    void StartDialogue()
    {
        index = 0;
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
            cutscene = false;
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
