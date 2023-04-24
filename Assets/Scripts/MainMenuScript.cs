using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        controlsmenu.SetActive(false);
        playbutton.SetActive(true);
        controlsbutton.SetActive(true);
        settingsbutton.SetActive(true);
        quitbutton.SetActive(true);
        creditsbutton.SetActive(true);
        SubmenuBackground.SetActive(false);
    }

    //credits button
    public void ShowSettings()
    {
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
        settingsmenu.SetActive(false);

        playbutton.SetActive(true);
        controlsbutton.SetActive(true);
        settingsbutton.SetActive(true);
        quitbutton.SetActive(true);
        creditsbutton.SetActive(true);
        SubmenuBackground.SetActive(false);
    }

    //credits button
    public void ShowCredits()
    {
        creditsMenu.SetActive(true);
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
        creditsMenu.SetActive(false);

        playbutton.SetActive(true);
        controlsbutton.SetActive(true);
        settingsbutton.SetActive(true);
        quitbutton.SetActive(true);
        creditsbutton.SetActive(true);
        SubmenuBackground.SetActive(false);
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
