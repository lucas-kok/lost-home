using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasse : MonoBehaviour
{

    // Making the variables
    public GameObject player;
    public PlayerLife playerLife;

    public float speed;
    public float triggerdistance;
    public float stopdistance;

    public float distance;
    public bool alive;
    public bool attack;


    // Start is called before the first frame update
    void Start()
    {
        // Assigning the variables
        player = GameObject.Find("Player");
        playerLife = GameObject.Find("Player").GetComponent<PlayerLife>();
        attack = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting true or false if the player is alive
        alive = playerLife.alive;

        // Checking if the player is alive
        if (alive == true)
        {
            // Calculating the distance between the player and the chaser
            distance = Vector2.Distance(transform.position, player.transform.position);

            // If the distance is between the triggerdistance and stop distance the chaser needs to move towards the player
            if (distance < triggerdistance)
            {
                if (distance > stopdistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                }
            }

            // Damaging the player if he is in de hitzone
            if (distance < stopdistance && attack == true)
            {
                playerLife.juice -= 0.5f;
                StartCoroutine(Cooldown());

                StartCoroutine(Destroy());

            }
        }

       

    }

    // Cooling down timer
    public IEnumerator Cooldown()
    {
        attack = false;

        yield return new WaitForSeconds(0.2f);

        attack = true; 
    }


    public IEnumerator Destroy()
    {
      

        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
        
    }


}
