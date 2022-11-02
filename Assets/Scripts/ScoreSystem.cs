using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreSystem
{
    public static event Action<int> OnScoreChanged;
    static int _score;

    public static void Add(int points)
    {
        _score += points;
        OnScoreChanged?.Invoke(_score);
        Debug.Log($"Score = {_score}");
    }
}
