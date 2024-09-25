using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioMixerSnapshot defaultSnapshot;
    [SerializeField] private AudioMixerSnapshot silencedSnapshot;
    [SerializeField] private AudioMixerSnapshot volumeOffSnapshot;

    [Inject]
    private UITimer gameoverScript;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if(gameoverScript != null)
        {
            gameoverScript.OnGameOver += SilenceMusic;
        }

        defaultSnapshot.TransitionTo(1f);
    }
    public void SilenceMusic()
    {
        silencedSnapshot.TransitionTo(1f);
    }
    public void TurnOffVolume()
    {
        volumeOffSnapshot.TransitionTo(1f);
    }
}
