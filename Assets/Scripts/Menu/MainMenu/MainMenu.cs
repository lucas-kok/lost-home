using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    // Functions for the Main Menu


    // Playing the game when playbutton is clicked, needs to get changed to the level the player reached
    public void PlayGame()
    {
        // Assigning the variables
        SceneManager.LoadScene("Design");
    }

    // Closing the game
    public void EndGame()
    {
        Application.Quit();
    }

}
