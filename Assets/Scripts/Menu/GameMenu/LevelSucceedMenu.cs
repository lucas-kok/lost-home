using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSucceedMenu : MonoBehaviour
{

    // Functions for the Succeed UI


    // Making the variables
    public SceneManagerScript sceneManagerScript;
    

    // Start is called before the first frame update
    public void Start()
    {
        // Assigning the variables
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    }

    // Opening the menu scene
    public void Menu()
    {
        sceneManagerScript.Menu();
    }

    // Restarting the level
    public void Restart()
    {
        sceneManagerScript.PlayAgain();
    }
    
    // Going to the next level
    public void Next()
    {
        sceneManagerScript.Next();
    }

}
