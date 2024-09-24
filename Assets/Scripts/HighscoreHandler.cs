using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour
{
    public static void SetHighscore(int newScore)
    {
        PlayerPrefs.SetInt("HighestScore", newScore);
    }
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighestScore", 0);
    }
}
