using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

public class BuffAtributesHolder : MonoBehaviour
{
    [Header("Magnet")]
    [SerializeField] public Transform magnetVFXTransform;
    [SerializeField] public AudioClip magnetPickUpSFX;
    [SerializeField] private Color magnetColor;

    [Header("Rainbow")]
    [SerializeField] public ParticleSystem rainbowVFX;
    [SerializeField] public AudioClip rainbowPickUpSFX;
    [SerializeField] private Color rainbowColor;

    [Header("Multiplier")]
    [SerializeField] public Animator txtAnimator;
    [SerializeField] public ParticleSystem multiplierVFX;
    [SerializeField] public AudioClip multiplierPickUpSFX;
    [SerializeField] private Color multiplierColor;

    [Space(15)]
    [SerializeField] private GameObject pickupTetxVFX;

    [Inject]
    private PlayerBehaviour playerBeh;

    private Transform playerTransform;
    private float screenBorder;
    private void Awake()
    {
        magnetVFXTransform.gameObject.SetActive(false);

        playerTransform = playerBeh.GetTransform();
    }
    private void Start()
    {
        screenBorder = UIScreenBoundries.Instance.GetBoundries().x;
    }
    public void SpawnTextVFX(AbstractBuff buff)
    {
        string vfxName = "";
        Color vfxColor = Color.white;
        Vector2 spawnPosition = playerTransform.position;
        float offset = 0;

        switch (buff)
        {
            case RainbowBuff:
                vfxColor = rainbowColor;
                vfxName = "PALETTE";
                offset = 1.385f;
                break;

            case MagnetBuff:
                vfxColor = magnetColor;
                vfxName = "MAGNET";
                offset = 1.24f;
                break;

            case MultiplierBuff:
                vfxColor = multiplierColor;
                vfxName = "MULTIPLIER";
                offset = 1.53f;
                break;
        }

        spawnPosition.x = Mathf.Clamp(spawnPosition.x, -screenBorder + offset, screenBorder - offset);
        Transform vfx = Instantiate(pickupTetxVFX, spawnPosition, Quaternion.identity).transform;

        TextMeshPro textVFX = vfx.GetComponent<TextMeshPro>();
        textVFX.color = vfxColor;
        textVFX.SetText(vfxName);
    }
}
