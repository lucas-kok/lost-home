using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerData : MonoBehaviour
{
    // Script for saving and loading data

    
    // Global variables
    public float levelReached;
    public float gems;
    public float timesPlayed;
    public float volume;
    public float theFirstTime;

    // Ingame variables


    // Menu variables
    public Button[] levelButtons;

    public float[] itemsValue;
    public float[] itemsPrice;
    public float[] gemCollected;

    public bool levelsLoaded;

    public float[] energyFase;
    public float[] energyNeeded;
    public float[] levelEnergy;
    
    public float faseReached;

    public bool alive;

    public bool inGame;
    public bool menu;


    // Start is called before the first frame update
    public void Start()
    {
        // Assigning the variables
        energyFase = new float[2];
        energyNeeded = new float[2];
        levelEnergy = new float[18];

        gemCollected = new float[30];

        // Loading the energy needed for fases
        energyNeeded[0] = 500f;
        energyNeeded[1] = 600f;

        Load();
    }
    
    // Function to save the player's progress
    public void Save()
    {
        // Global
        PlayerPrefs.SetFloat("FaseReached", faseReached); // Setting the faseReached variable
        PlayerPrefs.SetFloat("Volume", volume); // Saving the player's volume
        PlayerPrefs.SetFloat("TheFirstTime", theFirstTime);

        // Ingame
        if (inGame == true)
        {
            // Checking if the player is still alive
            if (alive == true)
            {
                Debug.Log(gems);
                PlayerPrefs.SetFloat("Gems", gems);
            }

            // Saving what gems the player has collected
            for (int i = 0; i < gemCollected.Length; i++)
            {
                PlayerPrefs.SetFloat("Gem" + i.ToString(), gemCollected[i]);
            }

            PlayerPrefs.SetFloat("LevelReached", levelReached);

            PlayerPrefs.SetFloat("TimesPlayed", timesPlayed);

            // Saving all the fase energy Data
            for (int i = 0; i < energyFase.Length; i++)
            {
                PlayerPrefs.SetFloat("EnergyFase" + i.ToString(), energyFase[i]);
            }

            // Saving all the level energy Data
            for (int i = 0; i < levelEnergy.Length; i++)
            {
                PlayerPrefs.SetFloat("LevelEnergy" + i.ToString(), levelEnergy[i]);
            }

        }

        // In Menu 
        if (menu == true)
        {
            PlayerPrefs.SetFloat("Gems", gems);
            PlayerPrefs.SetFloat("LevelReached", levelReached);

            // Saving the data for the shop

            // Saving the data if the player has the item or not (0 = false | 1 = true)
            for (int i = 0; i < itemsValue.Length; i++)
            {
                PlayerPrefs.SetFloat("Item" + i.ToString(), itemsValue[i]);
            }

        }        

    }

    // Function to load the player's progress
    public void Load()
    {
        // Global
        volume = PlayerPrefs.GetFloat("Volume", 0);
        levelReached = PlayerPrefs.GetFloat("LevelReached", 1);
        theFirstTime = PlayerPrefs.GetFloat("TheFirstTime", 0);

        if (theFirstTime == 0 && GameObject.Find("MenuManager") != null)
        {
            GameObject.Find("MenuManager").GetComponent<MenuManager>().PlayTurtorial();
            theFirstTime = 1;
            Updater();
        }

        // Loading all the level energy Data
        for (int i = 0; i < levelEnergy.Length; i++)
        {
            levelEnergy[i] = PlayerPrefs.GetFloat("LevelEnergy" + i.ToString(), 0);
        }


        // Loading the player's fase energy Data
        for (int i = 0; i < energyFase.Length; i++)
        {
            energyFase[i] = PlayerPrefs.GetFloat("EnergyFase" + i.ToString(), 0);
        }

        // Loading the global variables
        faseReached = PlayerPrefs.GetFloat("FaseReached", 0); // Getting the faseReached variable
        gems = PlayerPrefs.GetFloat("Gems", 0);

        // Ingame
        if (inGame == true)
        {

            // Loading what gems the player has collected
            for (int i = 0; i < gemCollected.Length; i++)
            {
                gemCollected[i] = PlayerPrefs.GetFloat("Gem" + i.ToString(), 0);
            }

            // Loading what item is equiped
            for (int i = 0; i < itemsValue.Length; i++)
            {
                itemsValue[i] = PlayerPrefs.GetFloat("Item" + i.ToString(), 0);
            }

            timesPlayed = PlayerPrefs.GetFloat("TimesPlayed", 0);
        }

        // In Menu
        if (menu == true)
        {
            gems = PlayerPrefs.GetFloat("Gems", 0);
                            
            for (int i = 0; i < itemsValue.Length; i++)
            {

                itemsValue[i] = PlayerPrefs.GetFloat("Item" + i.ToString(), 0);

            }

        }
        

    }

    // Function to update the Player's Data
    public void Updater()
    {
        Save();
        Load();
    }

    public void LoadLevelEnergy()
    {
        
    }
}
