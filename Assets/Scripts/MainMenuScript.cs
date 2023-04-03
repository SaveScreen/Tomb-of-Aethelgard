using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private GameObject mainmenu;
    public GameObject controlsmenu;

    void Awake()
    {
        mainmenu = Instantiate(controlsmenu);
        mainmenu.transform.SetParent(transform);
        controlsmenu.SetActive(false);
    }
    public void ShowControls()
    {
        controlsmenu.SetActive(true);
    }

    public void HideControls()
    {
        controlsmenu.SetActive(false);
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
