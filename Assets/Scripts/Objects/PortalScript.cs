using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{

    // Making all the variables
    public GameObject player;
    public Rigidbody2D rb;
    public PlayerLife playerLife;
    public PlayerMovement playerMovement;

    public GameObject portal1;
    public GameObject portal2;

    public GameObject portal1Spawner;
    public GameObject portal2Spawner;

    public GameObject portal1Angle;
    public GameObject portal2Angle;

    public BoxCollider2D boxCollider2DPortal1;
    public BoxCollider2D boxCollider2DPortal2;

    public float distancePortal1;
    public float distancePortal2;

    public GameObject offsetPoint;

    public float forwardForce;
    public float angle;

    public float rotation1;
    public float rotation2;

    public float zRotation1;
    public float zRotation2;

    public bool teleporting;

    public float minSpawnRotation;
    public float maxSpawnRotation;



    // Start is called before the first frame update
    void Start()
    {
        
        // Assigning the variables
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody2D>();
        playerLife = player.GetComponent<PlayerLife>();
        playerMovement = player.GetComponent<PlayerMovement>();

        portal1 = GameObject.Find("Portal1");
        portal2 = GameObject.Find("Portal2");

        portal1Spawner = GameObject.Find("Portal1Spawner");
        portal2Spawner = GameObject.Find("Portal2Spawner");

        portal1Angle = GameObject.Find("Portal1Angle");
        portal2Angle = GameObject.Find("Portal2Angle");

        boxCollider2DPortal1 = portal1.GetComponent<BoxCollider2D>();
        boxCollider2DPortal2 = portal2.GetComponent<BoxCollider2D>();

        offsetPoint = GameObject.Find("OffsetPoint");

        zRotation1 = portal1.transform.eulerAngles.z;
        zRotation2 = portal2.transform.eulerAngles.z;

        teleporting = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerLife.alive == true)
        {
            // Calculating the distance between the player and both portals
            distancePortal1 = Mathf.Round(Vector2.Distance(player.transform.position, portal1Spawner.transform.position) * 10) / 10;
            distancePortal2 = Mathf.Round(Vector2.Distance(player.transform.position, portal2Spawner.transform.position) * 10) / 10;
        }
        
        // Setting the first offset point for calculating the angle
        if (distancePortal1 == 0.6f || distancePortal2 == 0.6f)
        {
            offsetPoint.transform.position = player.transform.position;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        // Start teleporting when the portal collides with the player
        if (col.gameObject.tag == "Player")
        {

            StartCoroutine(Teleport());

        }

    }

    // Teleporting IEnumerator function
    public IEnumerator Teleport()
    {

        playerLife.juiceDrain = false; // Deactivating juice drain to prevent dieing from teleporting

        // What to do if the player hits portal1
        if (distancePortal1 < distancePortal2 && teleporting == true)
        {

            teleporting = false; // Setting teleportation to false

            portal1Angle.transform.position = player.transform.position; // Setting the 2nd point for calculating the angle

            // Calculating the rotation the other portal must rotate
            angle = Mathf.Round(Mathf.Atan2(offsetPoint.transform.position.y - portal1Angle.transform.position.y, offsetPoint.transform.position.x - portal1Angle.transform.position.x)*-180 / Mathf.PI / 10) * 10;
            rotation2 = 90 - angle;

            // Checking if the rotation is not outside the minimum or maximum
            if (rotation2 <= minSpawnRotation && rotation2 < maxSpawnRotation)
            {
                rotation2 = minSpawnRotation;
            }
            if (rotation2 >= maxSpawnRotation && rotation2 > minSpawnRotation)
            {
                rotation2 = maxSpawnRotation;
            }

            boxCollider2DPortal2.enabled = false; // Disabling the other portal's collider to prevent going back

            portal2Spawner.transform.Rotate(0, 0, rotation2); // Rotating the spawner

            player.transform.position = portal2Spawner.transform.position; // Teleporting the player to the other portal

            rb.velocity = transform.up * 0f; // Setting the player's velocity to zero to freeze the current velocity

            rb.velocity = portal2Spawner.transform.up * forwardForce; // Shooting the player in the calculated direction

            yield return new WaitForSeconds(0.3f);

            portal2Spawner.transform.localEulerAngles = new Vector3(0 ,0 ,zRotation2); // Resetting the Spawners' rotation back to its original

        }


        // What to do if the player hits portal2
        if (distancePortal1 > distancePortal2 && teleporting == true)
        {

            teleporting = false; // Setting teleportation to false

            portal2Angle.transform.position = player.transform.position; // Setting the 2nd point for calculating the angle

            // Calculating the rotation the other portal must rotate
            angle = Mathf.Round(Mathf.Atan2(offsetPoint.transform.position.y - portal2Angle.transform.position.y, offsetPoint.transform.position.x - portal2Angle.transform.position.x)*180 / Mathf.PI / 10) * 10;
            rotation1 = (90 - angle) * -1;

            // Checking if the rotation is not outside the minimum or maximum
            if (rotation1 <= minSpawnRotation && rotation1 < maxSpawnRotation)
            {
                rotation1 = minSpawnRotation;
            }
            if (rotation1 >= maxSpawnRotation && rotation1 > minSpawnRotation)
            {
                rotation1 = maxSpawnRotation;
            }

            boxCollider2DPortal1.enabled = false; // Disabling the other portal's collider to prevent going back

            portal1Spawner.transform.Rotate(0, 0, rotation1); // Rotating the spawner

            player.transform.position = portal1Spawner.transform.position; // Teleporting the player to the other portal

            rb.velocity = transform.up * 0f; // Setting the player's velocity to zero to freeze the current velocity

            rb.velocity = portal1Spawner.transform.up * -forwardForce; // Shooting the player in the calculated direction

            yield return new WaitForSeconds(0.3f);

            portal1Spawner.transform.localEulerAngles = new Vector3(0 ,0 ,zRotation1); // Resetting the Spawners' rotation back to its original

        }

        yield return new WaitForSeconds(0.2f);

        ResetColliders(); // Resetting the colliders

    }

    // Void to reset the colliders back to true
    public void ResetColliders()
    {

        teleporting = true;

        boxCollider2DPortal1.enabled = true;
        boxCollider2DPortal2.enabled = true;

    }
}
