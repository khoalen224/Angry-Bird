using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;


    public void UpdateScore(ScoreManager scoreManager)
    {
        scoreText.text = $"{scoreManager.score}";
    }
}
