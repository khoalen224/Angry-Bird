using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events; 
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public UnityEvent OnScoreChanged;

    public int score { get; private set; }

    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke();
    }

    public void AddScoreForRemainingBirds(int remainingBirds)
    {
        int bonus = remainingBirds * 10000;
        AddScore(bonus);
    }
}
