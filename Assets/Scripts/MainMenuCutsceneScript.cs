using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MainMenuCutsceneScript : MonoBehaviour
{
    public CanvasGroup canvasalpha;
    public GameObject menucanvas;
    public GameObject creditscanvas;
    public GameObject menudirector;
    public TextMeshProUGUI textbox;
    public string[] lines;
    private int index;
    public InputAction menuinput;
    private bool menustarter;
    private bool menustarted;
    private float aspeed;
    private bool fadeout;

    // Start is called before the first frame update
    void Start()
    {
        menustarted = false;
        menucanvas.SetActive(false);
        creditscanvas.SetActive(true);
        menudirector.SetActive(false);
        textbox.text = string.Empty;
        canvasalpha.alpha = 0;
        index = 0;
        aspeed = 0.5f;
    }

    void OnEnable() {
        menuinput.Enable();
    }

    void OnDisable() {
        menuinput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!menustarted) {
            menustarter = menuinput.IsPressed();

            if (index < lines.Length) {
                WriteText();   
            } 
            else {
                menustarted = true;
            }
        }
        

        if (menustarter) {
            menustarted = true;
        }

        if (menustarted) {
            menucanvas.SetActive(true);
            creditscanvas.SetActive(false);
            menudirector.SetActive(true);
        }
    }

    void WriteText() {
        textbox.text = lines[index];
                if (fadeout == false) {
                    if (canvasalpha.alpha < 1.0f) {
                        canvasalpha.alpha += aspeed * Time.deltaTime;
                    }
                    else {
                        StartCoroutine(WaitForFade());
                    }
                } 
                else {
                    if (canvasalpha.alpha > 0) {
                        canvasalpha.alpha -= aspeed * Time.deltaTime;
                    }
                    else {
                        fadeout = false;
                        index ++;
                    }
                }
    }

    IEnumerator WaitForFade() {
        yield return new WaitForSeconds(2f);
        fadeout = true;
        
    }
}