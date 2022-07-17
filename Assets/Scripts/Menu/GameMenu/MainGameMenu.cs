using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameMenu : MonoBehaviour
{
    // Functions for the ingame UI


    // Making the variables 
    public SceneManagerScript sceneManagerScript;


    // Start is called before the first frame update
    public void Start()
    {
        // Assigning the variables
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    }

    // Pausing the game
    public void Pause()
    {
        sceneManagerScript.Pause();
    }
}
