using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    public void AddPoint()
    {
        score += 1;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Kills: " + score;
    }

   
}
