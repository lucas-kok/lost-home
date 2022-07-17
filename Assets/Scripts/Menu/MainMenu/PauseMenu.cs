using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Closing the game
    public void EndGame()
    {
        Application.Quit();
    }

    // Resuming the game
    public void Resume()
    {
        Time.timeScale = 1;
    }

    // Pausing the game
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    // Opening the menu
    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}
