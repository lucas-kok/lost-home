using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gems : MonoBehaviour
{
    
    // Making the variables
    public Text gemText;
    public float gems;
    public float[] gemCollected;

    public SceneManagerScript sceneManagerScript;
    public PlayerData playerData;


    public void Start()
    {
        // Assigning the variables
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
        playerData = GameObject.Find("SceneManager").GetComponent<PlayerData>();

        gemText = GameObject.Find("GemText").GetComponent<Text>();
        gemCollected = new float[30];
        gems = playerData.gems;

        // Loading what gems the player has collected
        for (int i = 0; i < gemCollected.Length; i++)
        {
            gemCollected[i] = PlayerPrefs.GetFloat("Gem" + i.ToString(), 0);

            // Checking if the player already collected the gem
            if (gemCollected[i] == 1)
            {
                // Checking if the gem is in the current level
                if (GameObject.Find(i.ToString()) != null)
                {
                    // Removing the gem that is collected
                    GameObject.Find(i.ToString()).SetActive(false);
                }
            }
        }

    }

    // Adding a gem
    public void AddGem()
    {
        // Adding a gem and updating the GemText
        GameObject.Find("SceneManager").GetComponent<PlayerData>().gems += 1;
        gemText.text = gems.ToString();

        playerData.Updater();
    }

    // CollisionVoid that checks if it gets hit by the player
    void OnCollisionEnter2D(Collision2D col)
    {

        // Setting the gem to true in playerdata
        if (col.gameObject.tag == "Gem")
        {
            AddGem();

            // Setting the collected gem to 1 
            playerData.gemCollected[int.Parse(col.gameObject.name)] = 1;

            playerData.Updater();
        }
        

    }

    // Updating the ingame text to the player's gems
    public void Update()
    {
        gems = playerData.gems;
        gemText.text = gems.ToString();
    }

}
