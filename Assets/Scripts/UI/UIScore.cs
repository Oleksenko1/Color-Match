using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class UIScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int scoreIncrease;
    [SerializeField] private int scoreReduce;

    [Inject]
    private PlayerBehaviour playerBehaviour;

    private int currentScore = 0;

    private void Start()
    {
        SetText();

        playerBehaviour.OnColorCollect += ChangeScore;
    }
    private void ChangeScore(bool isRightColor)
    {
        if (isRightColor)
        {
            currentScore += scoreIncrease;
        }
        else
        {
            currentScore -= scoreReduce;
        }

        // Clamps score, so it doesn't go lower 0
        currentScore = Mathf.Clamp(currentScore, 0, 9999);

        SetText();
    }
    private void SetText()
    {
        text.SetText(currentScore.ToString());
    }

    public int GetScore() { return currentScore; }
}
