using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))] // Only goes forward if the object has a Line Renderer
public class DragNShoot : MonoBehaviour
{
    // Script to play on the pc

    // Making the variables
    public float power = 10f;
    public Rigidbody2D rb;

    public Vector2 minPower;
    public Vector2 maxPower;

    Camera cam;
    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;

    public LineRenderer lr;
    public PlayerLife playerLife;


    // Awake function that assings all the variables from the player
    void Awake()
    {

        // Assigning the variables
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

        playerLife = GetComponent<PlayerLife>();

    }

    public void Start()
    {
        // Assigning the camera
        cam = Camera.main;
    }

    // Update is called once per frame
    public void Update()
    {
        // If mouse is down
        if (Input.GetMouseButtonDown(0))
        {

            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15f;

        }

        // If mouse is hold
        if (Input.GetMouseButton(0))
        {

            playerLife.juiceDrain = true;

            Vector3 dragPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }

        // If mouse is released
        if (Input.GetMouseButtonUp(0))
        {

            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15f;

            // Calculating and adding the force
            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
            rb.AddForce(force * power, ForceMode2D.Impulse);

        }

    }
}
