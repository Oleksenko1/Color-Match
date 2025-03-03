using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
public class UIHearts : MonoBehaviour
{
    public event Action OnGameOver;

    public event Action OnRevive;

    [SerializeField] private List<Animator> heartAnimators;
    [SerializeField] private float animationDelay = 0.5f;
    [SerializeField] private AudioClip healthUpSFX;

    [Inject]
    private PlayerBehaviour player;

    private int heartAmount;
    private int currentHearts;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("GameMode", 0) == 1)
        {
            heartAmount = heartAnimators.Count;

            currentHearts = heartAmount;

            Invoke(nameof(UpdateAllHearts), 1f);

            player.OnColorCollect += LooseHeart;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateAllHearts()
    {
        StartCoroutine(UpdateHeartsCoroutine());
    }
    IEnumerator UpdateHeartsCoroutine()
    {
        // Cicle throught every heart and activate their animation if needed
        for (int x = 1; x < currentHearts + 1; x++)
        {
            heartAnimators[x - 1].SetTrigger("Recover");
            SoundsHandler.PlaySFX(healthUpSFX, 1f);
            yield return new WaitForSeconds(animationDelay);
        }
    }
    private void LooseHeart(bool correctColor)
    {
        if (correctColor) return;

        heartAnimators[currentHearts - 1].SetTrigger("Die");
        currentHearts--;

        // Game over if there are less than 1 heart
        if(currentHearts <= 0)
        {
            OnGameOver?.Invoke();
        }
    }
}
