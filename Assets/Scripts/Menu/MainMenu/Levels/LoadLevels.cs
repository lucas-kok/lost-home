using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;

using UnityEngine.EventSystems;

public class LoadLevels : MonoBehaviour
{
    
    // Making the variables
    public float levelReached;
    public bool levelsLoaded;

    public float[] levelEnergy;
    public Button[] levelButtons;
    

    public PlayerData playerData;


    // Start is called before the first frame update
    public void Start()
    {
        // Assigning the variables 
        playerData = GameObject.Find("MenuManager").GetComponent<PlayerData>();

        levelsLoaded = playerData.levelsLoaded;
        levelReached = playerData.levelReached;
        levelEnergy = playerData.levelEnergy;
        levelButtons = playerData.levelButtons;
    }

    // Update is called once per frame
    public void Update()
    {

        // Checking if the levels aren't loaded yet and if the Level Canvas is visible
        if (levelsLoaded == false && GameObject.Find("Levels") != null)
        {
            // Loading in the levels
            for (int i = 0; i < levelButtons.Length; i++)
            {   

                // If the player already reached the level, its unlocked, otherwise its locked
                if ((i + 1) <= levelReached)
                {
                    // Removing the lock icon
                    GameObject.Find("LockIcon" + (i + 1).ToString()).SetActive(false);

                    // Setting the right energy 
                    GameObject.Find((i + 1).ToString()).transform.GetChild(3).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = levelEnergy[i].ToString() + "%";
                } else
                {
                    levelButtons[i].interactable = false;
                }                
            
                levelsLoaded = true;


            }
        }            
        
    }

    // Starting the level the player has clicked
    public void GoToLevel()
    {
        SceneManager.LoadScene (EventSystem.current.currentSelectedGameObject.name);
    }

    
    public void Pepijn()
    {
        SceneManager.LoadScene("pepijn");
    }

}
