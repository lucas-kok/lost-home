using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    // Making the variables
    public GameObject player;
    public float distance;
    public float damage;
    public float damageScale;

    public float dangerDistance;
    public float explosionDistance;

    public SpriteRenderer spriteRenderer;
    public PlayerLife playerLife;

    public bool exploding;


    // Start is called before the first frame update
    void Start()
    {

        // Setting all the variables and or GameObjects
        player = GameObject.Find("Player");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerLife = GameObject.Find("Player").GetComponent<PlayerLife>();

        exploding = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        // First checking if the player is alive
        if (playerLife.alive == true)
        {
            // Calculating the distance between the player and explosion
            distance = Mathf.Round(Vector2.Distance(player.transform.position, transform.position) * 10) / 10;

            // What will happen when the player gets in the danger zone
            if (distance <= dangerDistance && distance > explosionDistance && exploding == false)
            {
                spriteRenderer.color =  new Color(255f, 69f, 0f);
            }

            // What will happen when the player gets in the explosion zone
            else if (distance <= explosionDistance && exploding == false)
            {
                spriteRenderer.color =  new Color(255f, 0f, 0f);

                StartCoroutine(TimeBomb());
            }
        }
        
    }

    // What need to happen if the player is in the explosion zone
    public IEnumerator TimeBomb()
    {

        exploding = true;

        yield return new WaitForSeconds(1f);

        Damage();
        Destroy(gameObject);

    }

    // Function to damage the player
    public void Damage()
    {
        // Checking if the player is in the damage distance
        if (distance < dangerDistance)
        {
            // Calculating the damage
            damage = Mathf.Round(50f - (distance / 0.3f * damageScale));

            // Draining the player's juice
            playerLife.juice -= damage;

        }
        
    } 
}
