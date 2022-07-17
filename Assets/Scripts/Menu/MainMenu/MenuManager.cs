using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{

    // Making the variables
    public PlayerData playerData;

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        // Assigning the variables
        playerData = gameObject.GetComponent<PlayerData>();
        playerData.menu = true;
        playerData.levelsLoaded = false;

        // Setting the volume
        audioMixer.SetFloat("volume", playerData.volume);
        playerData.Load();
        
    }

    public void Play()
    {

        SceneManager.LoadScene(playerData.levelReached.ToString());

    }

    public void PlayTurtorial()
    {

        SceneManager.LoadScene("Turtorial");

    }

    // Closing the game
    public void EndGame()
    {
        Application.Quit();
    }

}
