using System.Collections;
using UnityEngine;


public class Booster : MonoBehaviour
{

    // Making the variables
    public GameObject player;

    public GameObject booster;
    public CircleCollider2D circleCollider2D;
    public ParticleSystem explosion;

    public Rigidbody2D rb;

    public float forwardForce;
    public float waitingTime;

    public PlayerLife playerLife;


    // Start is called before the first frame update
    void Start()
    {
        // Assigning the variables
        player = GameObject.Find("Player");

        circleCollider2D = GetComponent<CircleCollider2D>();
        explosion = GameObject.Find("ExplosionParticle").GetComponent<ParticleSystem>();

        rb = player.GetComponent<Rigidbody2D>();
        playerLife = player.GetComponent<PlayerLife>();
        
    }

    // Function when the booster hit something
    void OnTriggerEnter2D(Collider2D col)
    {

        // Checking if the booster collided with the player
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(Boost());
        }


    }

    // Function that will shoot the player in the direction the booster is facing
    public IEnumerator Boost()
    {
        // Toggle the juicedrain so the speed doesn't affect the juice
        playerLife.juiceDrain = false;

        // Shooting the player in facing direction
        rb.velocity = (transform.up * forwardForce );

        // Playing the explosion particle
        explosion.Play();
        
        // Removing the booster for x seconds
        circleCollider2D.enabled = false;
        booster.SetActive(false);

        // Waiting for some time then playing the spawning particle
        yield return new WaitForSeconds(waitingTime);
        explosion.Play();

        yield return new WaitForSeconds(0.2f);

        // Respawning the booster
        circleCollider2D.enabled = true;
        booster.SetActive(true);


    }
}
