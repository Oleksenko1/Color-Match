using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPausePanel : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button unpausePanel;

    private void Awake()
    {
        pauseBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 0f;
            gameObject.SetActive(true);
        });

        unpausePanel.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
    }
}
