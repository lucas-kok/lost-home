using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public float score;
    public TextMeshProUGUI scoreText;

    public void Start()
    {
        scoreText = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void AddScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    public void Update()
    {
        Debug.Log(score);
        scoreText.text = score.ToString();
    }
}
