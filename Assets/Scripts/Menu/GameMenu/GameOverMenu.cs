using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{

    // Functions for the game over UI


    // Making the variables
    public SceneManagerScript sceneManagerScript;


    // Start is called before the first frame update
    void Start()
    {
        // Assigning the variables
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    }

    // Restarting the level
    public void Restart()
    {
        sceneManagerScript.PlayAgain();
    }

    // Opening the menu scene
    public void Menu()
    {
        sceneManagerScript.Menu();
    }
}
