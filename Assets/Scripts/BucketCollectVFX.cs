using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BucketCollectVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem vfxMain;
    [SerializeField] private ParticleSystem vfxAdditional;

    [Inject]
    private PlayerBehaviour player;

    ParticleSystem.MinMaxGradient vfxColor;

    private void Awake()
    {
        player.OnColorCollect += EmmitParticles;
        player.OnColorChanged += SetColor;
    }
    private void Start()
    {
        SetColor();
    }
    private void EmmitParticles(bool isRightColor)
    {
        if (!isRightColor) return;

        if (vfxMain != null && vfxAdditional != null)
        {
            var mainModule = vfxMain.main;
            var additionalModule = vfxAdditional.main;

            mainModule.startColor = vfxColor;
            additionalModule.startColor = vfxColor;

            vfxMain.Emit(10);
            vfxAdditional.Emit(10);
        }
        else
        {
            Debug.LogError("ParticleSystem is no assigned!");
        }
    }

    private void SetColor()
    {
        vfxColor = new ParticleSystem.MinMaxGradient(player.GetColor().color);
    }
}
