using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{

    public AudioMixer audioMixer;
    public PlayerData playerData;

    public GameObject volumePercentageText;

    public float volumePercentage;

    // Start is called before the first frame update
    public void Start()
    {
        // Assigning the variables
        playerData = GameObject.Find("MenuManager").GetComponent<PlayerData>();

        volumePercentageText = GameObject.Find("VolumePercentageText");

        // Setting the mixer volume and slider to the saved volume variable
        audioMixer.SetFloat("volume", playerData.volume);
        GameObject.Find("Volume").GetComponent<Slider>().value = playerData.volume;

        // Calculating and setting the volume%
        volumePercentage = (Mathf.Round(100 - ((playerData.volume / 80) * -100) / 10) * 10) - 900;
        volumePercentageText.GetComponent<TMPro.TextMeshProUGUI>().text = volumePercentage.ToString() + "%";
    }

    // Setting the audio to the chosen volume
    public void SetVolume (float volume)
    {
        // Rounding the volume variable
        volume = Mathf.Round(volume / 10) * 10; 

        // Setting the volume variable to the mixer
        audioMixer.SetFloat("volume", volume);
        playerData.volume = volume;

        // Calculating and setting the volume%
        volumePercentage = (Mathf.Round(100 - ((volume / 80) * -100) / 10) * 10) - 900;
        volumePercentageText.GetComponent<TMPro.TextMeshProUGUI>().text = volumePercentage.ToString() + "%";

        // Saving the playerData
        playerData.Updater();
    }
}
