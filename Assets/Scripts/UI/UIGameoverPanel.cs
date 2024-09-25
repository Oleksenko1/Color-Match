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
    [Header("SFX")]
    [SerializeField] private AudioClip playSFX;
    [SerializeField] private AudioClip toMenuSFX;

    [Inject] private UITimer timerScript;
    [Inject] private UIScore scoreScript;

    private void Start()
    {
        timerScript.OnGameOver += ShowPanel;

        gameObject.SetActive(false);

        InitializeButtons();
    }
    private void InitializeButtons()
    {
        restartBtn.onClick.AddListener(() =>
        {
            // Loads game scene
            SoundsHandler.PlaySFX(playSFX, 1f);
            UILoadingScreen.Instance.PlayLoadIn();
            MusicManager.Instance.TurnOffVolume();
            Invoke("RestartButton", 1f);
        });

        toMenuBtn.onClick.AddListener(() =>
        {
            // Loads menu scene
            SoundsHandler.PlaySFX(toMenuSFX, 1f);
            UILoadingScreen.Instance.PlayLoadIn();
            MusicManager.Instance.TurnOffVolume();
            Invoke("ToMenuButton", 1f);
        });
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
    private void ToMenuButton()
    {
        GameSceneLoader.LoadScene(GameSceneLoader.Scenes.MainMenu);
    }
    private void RestartButton()
    {
        GameSceneLoader.LoadScene(GameSceneLoader.Scenes.GameScene);
    }
}
