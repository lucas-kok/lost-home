using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    // Script for a swiping shop shop design

    // Making the variables
    public float faseReached;

    public GameObject scrollbar;
    float scrollpos = 0f;
    float[] pos;

    void Start()
    {
        faseReached = GameObject.Find("MenuManager").GetComponent<PlayerData>().faseReached;

        // Checking what fase the player has reached, than going to that fase ingame
        if (faseReached == 1)
        {
            scrollpos = 0.6f;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
    
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);

        for(int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scrollpos = scrollbar.GetComponent<Scrollbar>().value;
        } else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scrollpos < pos[i] + (distance / 2) && scrollpos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
            {
                if (scrollpos < pos[i] + (distance / 2) && scrollpos > pos[i] - (distance / 2))
                {   
                    transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                    for (int a = 0; a < pos.Length; a++)
                    {
                        if (a != i)
                        {
                            transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        }
                    }
                }
            }

    }
}
