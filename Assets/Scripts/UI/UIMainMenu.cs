using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMainMenu : MonoBehaviour
{
    [Header("Text fields")]
    [SerializeField] private TextMeshProUGUI highScoreTxt;
    [Header("Buttons")]
    [SerializeField] private Button playBtn;
    [SerializeField] private Button settingsBtn;
    [SerializeField] private Button exitBtn;
    [Header("SFX")]
    [SerializeField] private AudioClip uiSFX;
    [SerializeField] private AudioClip playSFX;
    [SerializeField] private AudioClip exitSFX;
    [Header("Other")]
    [SerializeField] private UIMenuSettingsPanel settings;

    private void Awake()
    {
        // Setting highest score amount
        highScoreTxt.SetText(HighscoreHandler.GetHighScore().ToString());
    }
    private void Start()
    {
        InitializeButtons();
    }
    private void InitializeButtons()
    {
        // Play button behaviour
        playBtn.onClick.AddListener(() =>
        {
            SoundsHandler.PlaySFX(playSFX, 1f);
            UILoadingScreen.Instance.PlayLoadIn();
            MusicManager.Instance.TurnOffVolume();
            Invoke("PlayButton", 1f);
        });

        // Settings button behaviour
        settingsBtn.onClick.AddListener(() =>
        {
            SoundsHandler.PlaySFX(uiSFX, 1f);
            settings.OpenPanel();
        });

        // Exit button behaviour
        exitBtn.onClick.AddListener(() =>
        {
            SoundsHandler.PlaySFX(exitSFX, 1f);
            UILoadingScreen.Instance.PlayLoadIn();
            MusicManager.Instance.TurnOffVolume();
            Invoke("ExitButton", 1f);
        });
    }
    private void PlayButton()
    {
        GameSceneLoader.LoadScene(GameSceneLoader.Scenes.GameScene);
    }
    private void ExitButton()
    {
        Application.Quit();
    }
}
