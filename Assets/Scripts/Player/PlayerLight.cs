using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    // Script for the player's light


    // Making the variables
    public PlayerLife playerLife;
    public GameObject pointLight2D;

    public Light2D light2D;

    public float juice;
    public float outerRadius;
    public float standardRadius;
    

    // Start is called before the first frame update
    void Start()
    {
        // Assigning the variables
        playerLife = GetComponent<PlayerLife>();
        light2D = pointLight2D.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Getting the juice
        juice = playerLife.juice;

        // Calculating the size of the radius, then assigning it
        outerRadius = Mathf.Round(juice) / 20f;
        light2D.pointLightOuterRadius = outerRadius + standardRadius;
    }

}
