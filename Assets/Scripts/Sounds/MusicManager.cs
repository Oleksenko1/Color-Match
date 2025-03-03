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
    private UITimer timer;
    [Inject]
    private UIHearts hearts;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        int gamemode = PlayerPrefs.GetInt("GameMode", 0);

        if (timer != null && hearts != null)
        {
            switch(gamemode)
            {
                case 0:
                    timer.OnGameOver += SilenceMusic;
                    break;
                case 1:
                    hearts.OnGameOver += SilenceMusic;
                    break;
            }
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
