using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MainMenuScript : MonoBehaviour
{
    private GameObject mainmenu;
    public GameObject controlsmenu;
    public GameObject settingsmenu;
    public GameObject creditsMenu;

    public GameObject playbutton;
    public GameObject controlsbutton;
    public GameObject settingsbutton;
    public GameObject quitbutton;
    public GameObject creditsbutton;
    public GameObject SubmenuBackground;

    public Button backsettings;
    public Button backcredits;
    public Button backcontrols;
    public Button settings;
    public Button credits;
    public Button controls;


    

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        controlsmenu.SetActive(false);
        settingsmenu.SetActive(false);
        creditsMenu.SetActive(false);
        SubmenuBackground.SetActive(false);
        
    }

    //controls button
    public void ShowControls()
    {
        backcontrols.Select();
        controlsmenu.SetActive(true);

        playbutton.SetActive(false);
        controlsbutton.SetActive(false);
        settingsbutton.SetActive(false);
        quitbutton.SetActive(false);
        creditsbutton.SetActive(false);
        SubmenuBackground.SetActive(true);
    }

    public void HideControls()
    {
        
        playbutton.SetActive(true);
        controlsbutton.SetActive(true);
        settingsbutton.SetActive(true);
        quitbutton.SetActive(true);
        creditsbutton.SetActive(true);
        controls.Select();

        SubmenuBackground.SetActive(false);
        controlsmenu.SetActive(false);
    }

    //credits button
    public void ShowSettings()
    {
        backsettings.Select();
        settingsmenu.SetActive(true);

        playbutton.SetActive(false);
        controlsbutton.SetActive(false);
        settingsbutton.SetActive(false);
        quitbutton.SetActive(false);
        creditsbutton.SetActive(false);
        SubmenuBackground.SetActive(true);
    }

    public void HideSettings()
    {
        playbutton.SetActive(true);
        controlsbutton.SetActive(true);
        settingsbutton.SetActive(true);
        quitbutton.SetActive(true);
        creditsbutton.SetActive(true);
        settings.Select();


        SubmenuBackground.SetActive(false);
        settingsmenu.SetActive(false);
    }

    //credits button
    public void ShowCredits()
    {
        backcredits.Select();
        creditsMenu.SetActive(true);

        playbutton.SetActive(false);
        controlsbutton.SetActive(false);
        settingsbutton.SetActive(false);
        quitbutton.SetActive(false);
        creditsbutton.SetActive(false);
        SubmenuBackground.SetActive(true);
    }
    public void HideCredits()
    {
        

        playbutton.SetActive(true);
        controlsbutton.SetActive(true);
        settingsbutton.SetActive(true);
        quitbutton.SetActive(true);
        creditsbutton.SetActive(true);
        credits.Select();

        SubmenuBackground.SetActive(false);
        creditsMenu.SetActive(false);
    }

    //play button
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //takes current scene and moves to next scene in scene order
        CutsceneScript.cutscene = true; //Plays cutscene at the very beginning of the game
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
