using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    // Script for the player's color


    // Making the variables
    public PlayerData playerData;
    public float[] itemsValue;
    float timesLoaded = 0f;

    public SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        // Assigning the variables
        playerData = GameObject.Find("SceneManager").GetComponent<PlayerData>();
        itemsValue = playerData.itemsValue;

        StartCoroutine(Load());

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        playerData.Load();
        // Checking what item is equiped, then giving the player the color assigned to it
        if (itemsValue[0] == 2f)
        {
            spriteRenderer.color =  new Color(255f, 0f, 100f);
        } else if (itemsValue[1] == 2f)
        {
            spriteRenderer.color =  new Color(255f, 255f, 0f);
        } else if (itemsValue[2] == 2f)
        {
            spriteRenderer.color =  new Color(0f, 3f, 255f);
        }
    }

    public IEnumerator Load()
    {

        playerData.Load();

        yield return new WaitForSeconds(0.05f);
        timesLoaded++;

        if (timesLoaded < 10)
        {
            StartCoroutine(Load());
        }

    }
}
