using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class UIGameoverPanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button toMenuBtn;
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI scoreAmountText;

    [Inject] private UITimer timerScript;
    [Inject] private UIScore scoreScript;

    private void Awake()
    {
        restartBtn.onClick.AddListener(() =>
        {
            // Loads game scene
            GameSceneLoader.LoadScene(GameSceneLoader.Scenes.GameScene);
        });

        toMenuBtn.onClick.AddListener(() =>
        {
            // Loads menu scene
            GameSceneLoader.LoadScene(GameSceneLoader.Scenes.MainMenu);
        });
    }
    private void Start()
    {
        timerScript.OnGameOver += ShowPanel;

        gameObject.SetActive(false);
    }

    private void ShowPanel()
    {
        // Sets score amount
        int currentScore = scoreScript.GetScore();
        scoreAmountText.SetText(currentScore.ToString());

        // Sets high score
        if(currentScore > HighscoreHandler.GetHighScore()) { HighscoreHandler.SetHighscore(currentScore); }

        gameObject.SetActive(true);
    }
}
