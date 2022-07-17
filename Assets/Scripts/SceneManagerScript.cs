using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;

public class SceneManagerScript : MonoBehaviour
{
    // Script to manage the levels


    // Making the variables
    public PlayerMovement playerMovement;

    public GameObject gameOverUI;
    public GameObject levelDone;
    public GameObject mainGameUI;

    public PlayerData playerData;
    public float levelReached;
    public float faseReached;

    public float energy;
    public float[] levelEnergy;
    public float[] energyNeeded;
    public float[] energyFase;
    public float[] gemCollected;

    // Start is called before the first frame update
    public void Start()
    {
        // Assigning the variables
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        mainGameUI = GameObject.Find("MainGame");

        playerData = gameObject.GetComponent<PlayerData>();
        playerData.inGame = true;

    }

    // Function to quit the game
    public void Quit()
    {
        Application.Quit();

        playerData.timesPlayed += 1;
        Updater();
    }

    // Function when a player dies
    public void Die()
    {
        playerData.alive = false;     
        
        // Setting the right UI
        mainGameUI.SetActive(false);
        levelDone.SetActive(true);

        levelDone.transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Level \nFailed";

        playerData.timesPlayed += 1;
        Updater();
        
    }

    // Function when the player finished an level
    public void Finish()
    {
        playerData.alive = false;
        
        playerMovement.enabled = false; // Disabling PlayerMovement

        levelEnergy = playerData.levelEnergy;
        levelReached = playerData.levelReached;

        energy = Mathf.Round(GameObject.Find("Player").GetComponent<PlayerLife>().juice) ;

        // Checking if the energy is not greater than 100
        if (energy > 100)
        {
            energy = 100;
        }
        
        // Setting the right UI
        mainGameUI.SetActive(false);
        levelDone.SetActive(true);

        levelDone.transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "Level  \nSucceed";

        energyFase = playerData.energyFase;

        // Getting the variables
        faseReached = playerData.faseReached;
        energyNeeded = playerData.energyNeeded;
        
        // Checking if the player has reached more energy than before
        if (energy > levelEnergy[(SceneManager.GetActiveScene().buildIndex - 1)])
        {

            // Checking to what the current level belongs to what fase, then add the energy
            if (SceneManager.GetActiveScene().buildIndex <= 9)
            {
                playerData.energyFase[0] -= levelEnergy[(SceneManager.GetActiveScene().buildIndex - 1)];
                playerData.energyFase[0] += energy; // Adding the right energy to the fase 1
            } else if (SceneManager.GetActiveScene().buildIndex > 9 && SceneManager.GetActiveScene().buildIndex <= 18)
            {
                playerData.energyFase[1] -= levelEnergy[(SceneManager.GetActiveScene().buildIndex - 1)];
                playerData.energyFase[1] += energy; // Adding the right energy to the fase 2
            } else if (SceneManager.GetActiveScene().buildIndex > 18 && SceneManager.GetActiveScene().buildIndex <= 27)
            {
                playerData.energyFase[2] -= levelEnergy[(SceneManager.GetActiveScene().buildIndex - 1)];
                playerData.energyFase[2] += energy; // Adding the right energy to the fase 3
            }
            

            // Fase regulator
            for (int i = 0; i < energyFase.Length; i++)
            {
                // Checking if the player can go to the next fase
                if (energyFase[i] >= energyNeeded[i])
                {
                    // Checking if the player reached a new fase
                    if (i == faseReached)
                    {
                        playerData.faseReached += 1f;
                        Debug.Log("Stage " + (i + 2).ToString() + " Unlocked");
                    }
                    
                }
            }
        } else // What to do if the player has less 
        {
            
        }

        playerData.levelEnergy[(SceneManager.GetActiveScene().buildIndex - 1)] = energy; // Setting the players energy to this level
        

        // Checking if the player is playing his higest level, then unlocking the next level
        if (SceneManager.GetActiveScene().buildIndex == levelReached)
        {
            playerData.levelReached += 1;
        }


        playerData.timesPlayed += 1;
        Updater(); // Updating the players data
        
    }

    // Function when the player wants to play again
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Function to go to the Menu
    public void Menu()
    {
        
        /*if (EventSystem.current.currentSelectedGameObject.name != "PauseMenuButton")
        {
            playerData.timesPlayed += 1;
            Updater();
        } */

        SceneManager.LoadScene("MainMenu");

        
    }

    // Function to go to the next level
    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }

    // Function to pause the game
    public void Pause()
    {
        playerMovement.levelDone = true;

        playerMovement.enabled = false; // Disabling PlayerMovement

    }

    // PlayerData Loading and Saving
    public void Updater()
    {
        playerData.Updater();
    }

    // Function to reset the players energy data (Will be deleted later)
    public void ResetEnergy()
    {
        levelEnergy = playerData.levelEnergy;
        energyFase = playerData.energyFase;
        gemCollected = playerData.gemCollected;

        // Resetting the players levels fase
        for (int i = 0; i < levelEnergy.Length; i++)
        {
            playerData.levelEnergy[i] = 0;
        }

        // Resetting the player's energy fase 
        for (int i = 0; i < energyFase.Length; i++)
        {
            playerData.energyFase[i] = 0;
        }

        for (int i = 0; i < gemCollected.Length; i++)
        {
            playerData.gemCollected[i] = 0;
        }


        playerData.faseReached = 0; // This won't be nessecery for launch because you can't go fase's back
        playerData.levelReached = 1;

        Updater(); // Saving the data
    }

}
