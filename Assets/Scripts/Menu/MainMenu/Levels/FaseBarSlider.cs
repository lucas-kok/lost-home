using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class FaseBarSlider : MonoBehaviour
{
    public PlayerData playerData;

    public Slider slider;
    
    public GameObject energyBar;

    public float[] energy;
    public float[] energyNeeded;
    public float fill = 0f;

    public float faseReached;

    // Start is called before the first frame update
    void Start()
    {
        // Assigning the variables
        playerData = GameObject.Find("MenuManager").GetComponent<PlayerData>();

        energy = playerData.energyFase;
        energyNeeded = playerData.energyNeeded;

        slider = gameObject.GetComponent<Slider>();
        faseReached = GameObject.Find("MenuManager").GetComponent<PlayerData>().faseReached;

        LoadFases(); // Starting to load the fases
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Function to load all the fases
    public void LoadFases()
    {
        // Removing the right locks
        for (int i = 0; i < energyNeeded.Length; i++)
        {
            if (i <= faseReached)
            {
                GameObject.Find("FaseLockIcon" + (i).ToString()).SetActive(false);
            }
        }

        // Loading all the energy data
        SetEnergy();

        // Starting the filling of the bar
        StartCoroutine(Fill());

    }

    // Function to set the right information on all the sliders
    public void SetEnergy()
    {
        for (int i = 0; i < energy.Length; i++)
        {
            energyBar = GameObject.Find("EnergyBar" + i.ToString());

            energyBar.transform.GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = energy[i].ToString() + "%"; // Setting the text of the EnergyBar
            energyBar.transform.GetChild(3).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = energyNeeded[i].ToString() + "%"; // Setting the energy needed for the bar

        }
    }

    // Function to slowly fill the energy bar
    public IEnumerator Fill()
    {
        yield return new WaitForSeconds(0.2f); // Delay so the canvas can load in

        for (int i = 0; i < energy.Length;)
        {
            // Proceeding to the next bar if the current bar is filled to certain amount
            if (i == faseReached)
            {
                if (fill >= energy[i] / energyNeeded[i]) // Calculating when the filling needs to stop
                {
                    i++;
                } else
                {
                    // Filling the bar
                    fill += energy[i] / 20000f;
                    GameObject.Find("EnergyBar" + i.ToString()).GetComponent<Slider>().value = fill;
                
                    // Small cooldown for the other fill
                    yield return new WaitForSeconds(0.01f);
                }
            } else // Setting the sliders of the fases that are not active shown
            {
                
                GameObject.Find("EnergyBar" + i.ToString()).GetComponent<Slider>().value = energy[i] / energyNeeded[i];
                i++;
            }
            
            
        }

    }
}
