using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Script for the playermovement on mobile


    // Making the variables
    public Rigidbody2D rb;
    public LineRenderer lr;

    public PlayerLife playerLife;

    public float power;
    public float maxDrag = 5f;

    // Rb floats that defines the drag when player gets juice
    public float drag;
    public float juiceDrag;
    
    Vector3 dragStartPos;
    Touch touch;

    public bool levelDone;
    

    // Start is called before the first frame update
    public void Start()
    {
        // Assigning the variables
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

        playerLife = GetComponent<PlayerLife>();

        rb.drag = drag;

        levelDone = false;
    }

    // Update function that checks if there is input from the mobile
    public void Update()
    {
        // Checking what the player is doing
        if (Input.touchCount > 0 && levelDone == false)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                DragRelease();
            }
        }

    }

    // Void when the player starts dragging
    void DragStart()
    {
        
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;

        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);


    }

    // Void when player is dragging
    void Dragging()
    {
        playerLife.juiceDrain = true; // Active Juice draining again

        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);

    }

    // Void when player stopped dragging
    void DragRelease()
    {

        lr.positionCount = 0;

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        rb.AddForce(clampedForce, ForceMode2D.Impulse);

    }



    // Function that slows the player down when getting juice
    public void DragVoid()
    {
        StartCoroutine(Drag());
    }

    public IEnumerator Drag()
    {

        rb.drag = juiceDrag;

        yield return new WaitForSeconds(0.5f);

        rb.drag = drag;

    }

}
