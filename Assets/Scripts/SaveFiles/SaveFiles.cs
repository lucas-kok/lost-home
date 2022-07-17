using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFiles : MonoBehaviour
{
    public float score;

    public void Update()
    {
        score = GameObject.Find("Text").GetComponent<Score>().score;
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("Score", score);
    }

    public void Load()
    {
        score = PlayerPrefs.GetFloat("Score");        
        GameObject.Find("Text").GetComponent<Score>().score = score;
    }
}
