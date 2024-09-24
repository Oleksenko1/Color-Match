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
    [Header("Other")]
    [SerializeField] private UIMenuSettingsPanel settings;

    private void Awake()
    {
        // Setting highest score amount
        highScoreTxt.SetText(HighscoreHandler.GetHighScore().ToString());

        // Play button behaviour
        playBtn.onClick.AddListener(() =>
        {
            GameSceneLoader.LoadScene(GameSceneLoader.Scenes.GameScene);
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
            Application.Quit();
        });
    }
}
