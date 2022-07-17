using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource energySound;
    public AudioSource selectSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEnergySound()
    {
        energySound.Play();   
    }

    public void PlaySelectSound()
    {
        selectSound.Play();
    }
}
