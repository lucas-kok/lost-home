using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    // Script for the player's life


    // Making the variables
    public Text juiceText;
    public float juice; 

    public float speed = 0f;
    public float speedMultiplier;

    Vector3 lastPosition;

    public bool juiceDrain;
    public bool alive;

    public SceneManagerScript sceneManagerScript; // Moet nog automatisch gebeuren
    public PlayerMovement playerMovement;

    private GameObject[] getCount;
    public float juiceCount;

    public bool TimerDraining;
    public bool finished;
    

    // Start is called before the first frame update
    void Start()
    {
        // Assigning the variables
        juiceDrain = true;
        alive = true;
        finished = false;

        // Finding all the game Objects
        juiceText = GameObject.Find("JuiceText").GetComponent<Text>();
        playerMovement = GetComponent<PlayerMovement>();

        juice = 25f;
        juiceText.text = juice.ToString();

        lastPosition = transform.position; //Creates a start position to measure the speed of the player

        getCount = GameObject.FindGameObjectsWithTag ("Juice");
        juiceCount = getCount.Length;

    }

    // Update method that checks if the player ran out of juice
    void Update()
    {
        // Setting the bool of the PlayerData
        GameObject.Find("SceneManager").GetComponent<PlayerData>().alive = alive;


        if (juice <= 0 && finished == false)
        {
            playerMovement.levelDone = true;
            Die();
            finished = true;
        }

    }

    // UpdateVoid that Drains the players' juice based on their speed
    void FixedUpdate()
    {

        speed = (transform.position - lastPosition).magnitude * speedMultiplier; // Rounds to whole number
        lastPosition = transform.position;

        // Draining the player's energy
        if (juice >= 0 && juice <= 100 && juiceDrain == true)
        {
            juice -= speed;
        }
        
        juiceText.text = Mathf.Round(juice).ToString() + "%";

        // Check if the player got 100% juice
        if (juice >= 100 && finished == false)
        {
            playerMovement.levelDone = true; // Sets the level on done 

            juiceDrain = false;
            sceneManagerScript.Finish(); // Must changed later to finish screen
            finished = true;
        }

        // Checking if the player has more than 100 juice if he collects all the juiceBulbs
        if (juiceCount == 0 && juice < 100 && finished == false)
        {
            playerMovement.levelDone = true; // Sets the level on done
            sceneManagerScript.Finish();

            finished = true;
        }

        
    }

    // Void that adds juice when the player hits an Juicy
    public void AddJuice()
    {
        StartCoroutine(TimerReset());

        juiceCount -= 1f;

        juice += 35f;

        if (juice <= 100)
        {
            juiceText.text = Mathf.Round(juice).ToString() + "%";
        }
        else if (juice > 100)
        {
            juiceText.text = "100%"; // Preventing that the juice will be 100+
        }

        playerMovement.DragVoid();

    }

    // Collision that kills the player when collides on spike
    void OnCollisionEnter2D(Collision2D col)
    {

       if (col.gameObject.tag == "Spike")
       {
            Die();
       }

    }

    // Function that kills the player & Restart the level
    public void Die()
    {
        alive = false;

        finished = true;

        juice = 0f;

        juiceText.text = juice.ToString() + "%";

        Destroy(gameObject);

        sceneManagerScript.Die();
    }

    // Currently not in usage
    public IEnumerator TimeDrainer()
    {
        if (TimerDraining == true)
        {
            juice -= 1f;
        }

        if (juice >= 40)
        {
            yield return new WaitForSeconds(1f);
        } else {
            yield return new WaitForSeconds(1.5f);
        }

        StartCoroutine(TimeDrainer());

    }

    public IEnumerator TimerReset()
    {

        TimerDraining = false;

        yield return new WaitForSeconds(1f);

        TimerDraining = true;
    }
    
}
