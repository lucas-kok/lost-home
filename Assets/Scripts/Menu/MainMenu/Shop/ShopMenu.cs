using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using TMPro;

public class ShopMenu : MonoBehaviour
{

    // Making the variables
    public PlayerData playerData;

    public int button;
    public GameObject[] shopButtons;

    public Button currentButton;
    public GameObject text;

    public float[] itemsPrice;
    public float[] itemsValue;

    public float gems;


    // Start is called before the first frame update
    void Start()
    {

        // Assigning the variables
        playerData = GameObject.Find("MenuManager").GetComponent<PlayerData>();
        playerData.Load();

        itemsValue = playerData.itemsValue;
        itemsPrice = playerData.itemsPrice;
    
    }

    // Update is called once per frame
    void Update()
    {
        itemsValue = playerData.itemsValue;
        itemsPrice = playerData.itemsPrice;

        if (GameObject.Find("Shop") != null)
        {
            // Setting the gems in the shop
            gems = playerData.gems;
            GameObject.Find("GemsText").GetComponent<TMPro.TextMeshProUGUI>().text = gems.ToString();
        }

        
        for (int i = 0; i < itemsValue.Length; i++)
        {
            // Checking if the player already has bought the item
            if (itemsValue[i] == 1f)
            {
                // Disabling the button 
                shopButtons[i].GetComponent<Button>().interactable = true;
                shopButtons[i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Equip";
            } else if (itemsValue[i] == 2f)
            {
                shopButtons[i].GetComponent<Button>().interactable = false;
                shopButtons[i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Using";
            } else
            {
                shopButtons[i].GetComponent<Button>().interactable = true;
                shopButtons[i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Buy";
            }

        }

    }

    // Function when the played clicks a buy button in the shop
    public void Button()
    {
        // Playing the selecting sound
        GameObject.Find("Sounds").GetComponent<Sound>().PlaySelectSound();

        // Checking what button is clicked    
        button = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        // Checking if the clicked button is already purchased or not
        if (itemsValue[button] == 0)
        {
            Buy();
        } else
        {
            Equip();
        }

    }

    // Function when the player wants to buy an item
    public void Buy()
    {
        // Recieving the gems
        gems = playerData.gems;

        // Check what button is clicked
        button = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        // Checking if the player has enough gems
        if (gems >= itemsPrice[button])
        {

            playerData.gems -= itemsPrice[button];

            // Setting the item to bought
            itemsValue[button] = 1;
            playerData.Updater();

            
        } else // What happens when the player hasn't enough items
        {
            // Empty yet
        }

    }

    // Function when the player already bought it and want to equip the item
    public void Equip()
    {
        // Check what button is clicked
        button = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        // Seting the clicked button as Active item
        itemsValue[button] = 2;

        // De-Equiping the current Equiped item
        for (int i = 0; i < itemsValue.Length; i++)
        {
            if (itemsValue[i] == 2 && i != button)
            {
                itemsValue[i] = 1;
            }
        }

        // Updating the player data
        playerData.Updater(); 

    }

}
