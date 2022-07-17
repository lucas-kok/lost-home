using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    // Script for the Ads

    
    // Making the variables
    public PlayerData playerData;
    public SceneManagerScript sceneManagerScript;
    public float timesPlayed;


    // Start is called before the first frame update
    public void Start()
    {
        // Assigning the variables
        Advertisement.Initialize("3911391", true);
        playerData = gameObject.GetComponent<PlayerData>();
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Recieving how many times the player played
        timesPlayed = playerData.timesPlayed;

        // When player played 3 times show an advertisement
        if (timesPlayed == 3)
        {
            Advertisement.Show();

            playerData.timesPlayed = 0;
            sceneManagerScript.Updater();
                        
        }

        // Function to prevent bugs, when the variable is greater than 3 setting it back to 1
        if (timesPlayed > 3)
        {
            playerData.timesPlayed = 1;
            sceneManagerScript.Updater();
            
        }
    }
}
