using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Turtorial : MonoBehaviour
{

    public GameObject[] compartments;
    public int currentActive = 0;
    bool canSkip = true;

    // Start is called before the first frame update
    void Start()
    {
        // Starting off with setting the first slider to active
        for (int i = 0; i < compartments.Length; i++)
        {
            if (i != currentActive)
            {
                compartments[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        // Checking if the player clicked to go to the next slider
        if (Input.GetKey(KeyCode.Mouse0) && canSkip == true && currentActive + 1 != compartments.Length)
        {
            Next();
        } else if (Input.GetKey(KeyCode.Mouse0) && currentActive + 1 == compartments.Length)
        {
            Skip();
        }

    }

    // Function to go to the next slider
    public void Next()
    {

        compartments[currentActive].SetActive(false);
        currentActive++;
        compartments[currentActive].SetActive(true);

        StartCoroutine(WaitForSkipping());

    }

    // Function to skip the turtorial
    public void Skip()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Function to prevent going to the next 3 sliders in 1 touch
    public IEnumerator WaitForSkipping()
    {

        canSkip = false;

        yield return new WaitForSeconds(0.3f);

        canSkip = true;

    }
}
