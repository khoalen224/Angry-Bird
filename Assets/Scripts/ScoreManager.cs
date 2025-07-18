using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class ScoreManager : MonoBehaviour
{
    public UnityEvent OnScoreChanged;
    public int score { get; private set; }
    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke();
    }
}
