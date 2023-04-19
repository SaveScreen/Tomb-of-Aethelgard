using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseScript : MonoBehaviour
{
    public GameObject pauseMenu;

    [Header("Win Screen")]
    public GameObject winScreen;
    //public GameObject winplatform;
    public static bool won = false;

    [Header("Lose Screen")]

    public GameObject loseScreen;
    public GameObject playerObject;
    private PlayerScript player;

    void Start()
    {
        player = playerObject.GetComponent<PlayerScript>();

        //pause menu panel
        pauseMenu.SetActive(false);

        winScreen.SetActive(false);

        loseScreen.SetActive(false);
    }

    public void PauseGame(){
        Time.timeScale = 0;
        player.SetPaused(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        AudioListener.pause = true;
    }

    public void ResumeGame(){
        Time.timeScale = 1.0f;
        player.SetPaused(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        AudioListener.pause = false;
    }

    public void GoToHub() {
        Time.timeScale = 1.0f;
        player.SetPaused(false);
        AudioListener.pause = false;
        SceneManager.LoadScene("HubLevel");
        pauseMenu.SetActive(false);
    }

    public void GoToMainMenu() {
        Time.timeScale = 1.0f;
        player.SetPaused(false);
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenuScene");
        pauseMenu.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void WinGame() {
        Time.timeScale = 0;
        player.SetWon(true);
        winScreen.SetActive(true);
    }
    public void LoseGame()
    {
        player.Die();
        loseScreen.SetActive(true);
    }
}
