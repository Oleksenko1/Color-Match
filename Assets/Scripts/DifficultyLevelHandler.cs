using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevelHandler : MonoBehaviour
{
    public static void SetLevel(int level)
    {
        PlayerPrefs.SetInt("DifficultyLevel", level);
    }

    public static int GetLevel()
    {
        return PlayerPrefs.GetInt("DifficultyLevel", 0);
    }
}
