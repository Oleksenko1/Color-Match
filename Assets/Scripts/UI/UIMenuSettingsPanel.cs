using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuSettingsPanel : MonoBehaviour
{
    [Header("Difficulty buttons")]
    [Tooltip("Put the buttons in ascending order of their difficulty")]
    [SerializeField] private List<Button> difficultyList;
    [Space(10)]
    [SerializeField] private Button closeBtn;
    [Header("SFX")]
    [SerializeField] private AudioClip closeSFX;
    [SerializeField] private AudioClip difficultyChangeSFX;

    private int currentDifficulty;
    private void Awake()
    {
        currentDifficulty = DifficultyLevelHandler.GetLevel();

        // Sets on click event to each difficulty level button
        for (int i = 0; i < difficultyList.Count; i++)
        {
            int index = i;
            difficultyList[i].onClick.AddListener(() => 
            {
                ChooseNewDifficulty(index);
                SoundsHandler.PlaySFX(difficultyChangeSFX, 1f);
            });
        }

        ChooseNewDifficulty(currentDifficulty);

        // Adds event to a close button
        closeBtn.onClick.AddListener(() =>
        {
            ClosePanel();
            SoundsHandler.PlaySFX(closeSFX, 1f);
        });


        ClosePanel();
    }
    private void ChooseNewDifficulty(int level)
    {
        currentDifficulty = level;

        DifficultyLevelHandler.SetLevel(currentDifficulty);

        for (int i = 0; i < difficultyList.Count; i++)
        {
            // Turns off - selected difficulty button
            difficultyList[i].interactable = i != currentDifficulty;
        }
    }
    public void OpenPanel() { gameObject.SetActive(true); }
    private void ClosePanel() { gameObject.SetActive(false); }
}
