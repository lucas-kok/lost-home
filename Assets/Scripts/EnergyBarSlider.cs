using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class EnergyBarSlider : MonoBehaviour
{
    public PlayerData playerData;

    public Slider slider;
    
    public GameObject energyBar;
    
    public float energy;
    public float fill;

    // Start is called before the first frame update
    void Start()
    {
        // Assigning the variables
        energy = Mathf.Round(GameObject.Find("Player").GetComponent<PlayerLife>().juice);

        // Preventing that the player will get over 100 energy
        if (energy > 100)
        {
            energy = 100;
        }

        // Starting to fill the energy bar
        StartCoroutine(Fill());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Fill()
    {
        if (fill == 0f)
        {
            GameObject.Find("EnergyBarText").GetComponent<TMPro.TextMeshProUGUI>().text = energy.ToString() + "%"; // Setting the energy the player got
            yield return new WaitForSeconds(0.2f); // Delay so the canvas can load in

        }

        if (fill < energy / 100)
        {
            // Filling the bar
            fill += energy / 20000f;
            GameObject.Find("EnergyBar").GetComponent<Slider>().value = fill;
        }
                
        // Small cooldown for the other fill
        yield return new WaitForSeconds(0.01f);

        // Restarting the fill
        StartCoroutine(Fill());

    }

}
