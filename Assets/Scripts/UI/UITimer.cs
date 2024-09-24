using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UITimer : MonoBehaviour
{
    public event Action OnGameOver;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int timeAmount = 80;
    [Header("SFX")]
    [SerializeField] private AudioClip gameoverSFX;
    [SerializeField] private AudioClip beepSFX;

    private int currentTime;
    private void Start()
    {
        currentTime = timeAmount;

        text.SetText(currentTime.ToString());

        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);

            currentTime--;

            text.SetText(currentTime.ToString());

            if(currentTime < 6 && currentTime > 0)
            {
                // Play sfx of countdown
                SoundsHandler.PlaySFX(beepSFX, 1f);
            }
        }

        OnGameOver?.Invoke();
        SoundsHandler.PlaySFX(gameoverSFX, 1f);
    }

}
