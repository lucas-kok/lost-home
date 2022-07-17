using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Script for all the collectable items


    // Making the variables
    public GameObject collectable;

    public GameObject player;
    public PlayerLife playerLife;
    public ParticleSystem particle;
    

    // Start is called before the first frame update
    void Start()
    {
        // Assigning the variables
        player = GameObject.Find("Player");
        playerLife = player.GetComponent<PlayerLife>();
    }

    // CollisionVoid that checks if it gets hit by the player
    void OnCollisionEnter2D(Collision2D col)
    {

        // Checking if the object hits the player
        if (col.gameObject.tag == "Player")
        {
            Explode();
        }

    }

    // Function what needs to happen when the player collects an item
    void Explode()
    {
        // Checking what item is collected

        if (gameObject.tag == "Gem")
        {
            //particle.Play();

            Destroy(gameObject);
        }

        if (gameObject.name == "Juice")
        {

            //particle.Play();

            // Fast check so the leven wont crash if the sound object isn't in the scene
            if (GameObject.Find("Sounds") != null)
            {
                GameObject.Find("Sounds").GetComponent<Sound>().PlayEnergySound();
            }
            
            playerLife.AddJuice();

            Destroy(collectable);

        }

    }
}
