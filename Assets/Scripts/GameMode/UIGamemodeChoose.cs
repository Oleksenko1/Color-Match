using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGamemodeChoose : MonoBehaviour
{
    [Header("Timer mode")]
    [SerializeField] private Button timerModeBtn;
    [TextArea]
    [SerializeField] private string timerModeDescription;

    [Header("Life mode")]
    [SerializeField] private Button lifeModeBtn;
    [TextArea]
    [SerializeField] private string lifeModeDescription;

    [Header("Sounds")]
    [SerializeField] private AudioClip chooseSFX;
    [SerializeField] private AudioClip startGameSFX;

    [Space(15)]
    [SerializeField] private Button startButton;

    [Space(15)]
    [SerializeField] private TextMeshProUGUI descriptionTMPro;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI gameModeLabel;
    private void Awake()
    {
        timerModeBtn.onClick.AddListener(() => 
        {
            SoundsHandler.PlaySFX(chooseSFX, 1f);
            ChangeModeTo(0, timerModeDescription, "Time mode");
        }
        );

        lifeModeBtn.onClick.AddListener(() =>
        {
            SoundsHandler.PlaySFX(chooseSFX, 1f);
            ChangeModeTo(1, lifeModeDescription, "Endless mode"); }
        );


        startButton.onClick.AddListener(() =>
        {
            SoundsHandler.PlaySFX(startGameSFX, 1f);
            UILoadingScreen.Instance.PlayLoadIn();
            MusicManager.Instance.TurnOffVolume();
            Invoke("StartGame", 1f); 
        });

        int choosenMode = PlayerPrefs.GetInt("GameMode", 0);

        switch (choosenMode)
        {
            case 0:
                ChangeModeTo(0, timerModeDescription, "Time mode");
                break;
            case 1:
                ChangeModeTo(1, lifeModeDescription, "Endless mode");
                break;
        }

        panel.SetActive(false);
    }

    private void ChangeModeTo(int i, string description, string label)
    {
        // Timer mode is 0, life mode is 1
        PlayerPrefs.SetInt("GameMode", i);

        descriptionTMPro.SetText(description);

        gameModeLabel.SetText(label);
    }

    public void Open()
    {
        panel.SetActive(true);
    }

    private void StartGame()
    {
        GameSceneLoader.LoadScene(GameSceneLoader.Scenes.GameScene);
    }
}
