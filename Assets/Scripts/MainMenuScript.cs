using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private GameObject mainmenu;
    public GameObject controlsmenu;
    public GameObject settingsmenu;

    void Awake()
    {
        controlsmenu.SetActive(false);
        settingsmenu.SetActive(false);
    }
    public void ShowControls()
    {
        controlsmenu.SetActive(true);
    }

    public void HideControls()
    {
        controlsmenu.SetActive(false);
    }
    public void ShowSettings()
    {
        settingsmenu.SetActive(true);
    }

    public void HideSettings()
    {
        settingsmenu.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //takes current scene and moves to next scene in scene order
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
