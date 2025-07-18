using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;
    int score = 0;
    int highscore = 0;
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
        highscoreText.text = "Highscore: " + highscore.ToString();
    }


}
