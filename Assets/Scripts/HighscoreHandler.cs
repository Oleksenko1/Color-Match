using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour
{
    public static void SetHighscore(int newScore, int gamemode)
    {
        switch (gamemode)
        {
            case 0:
                PlayerPrefs.SetInt("HighScore_Timer", newScore);
                break;

            case 1:
                PlayerPrefs.SetInt("HighScore_Endless", newScore);
                break;
        }

        PlayerPrefs.SetInt("HighestScore", newScore);
    }
    public static int GetHighScore(int gamemode)
    {
        switch (gamemode)
        {
            case 0:
                return PlayerPrefs.GetInt("HighScore_Timer", 0);

            case 1:
                return PlayerPrefs.GetInt("HighScore_Endless", 0);

            default:
                return -1;
        }
    }
}
