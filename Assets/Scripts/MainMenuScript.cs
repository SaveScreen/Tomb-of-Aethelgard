using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private GameObject mainmenu;
    public GameObject controlsmenu;
    public GameObject settingsmenu;
    public GameObject creditsmenu;

    void Awake()
    {
        controlsmenu.SetActive(false);
        settingsmenu.SetActive(false);
        creditsmenu.SetActive(false);
    }

    //controls button
    public void ShowControls()
    {
        controlsmenu.SetActive(true);
    }

    public void HideControls()
    {
        controlsmenu.SetActive(false);
    }

    //credits button
    public void ShowSettings()
    {
        settingsmenu.SetActive(true);
    }

    public void HideSettings()
    {
        settingsmenu.SetActive(false);
    }

    //credits button
    public void ShowCredits()
    {
        creditsmenu.SetActive(true);
    }
    public void HideCredits()
    {
        creditsmenu.SetActive(false);
    }
    //play button
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //takes current scene and moves to next scene in scene order
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
