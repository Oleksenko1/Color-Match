using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Inject]
    private PlayerBehaviour player;
    private void Start()
    {
        player.OnColorCollect += OnPlayerCollect;
    }
    private void OnPlayerCollect(bool isCorrectColor)
    {
        string triggerName = isCorrectColor ? "OnCorrectCollect" : "OnWrongCollect";

        animator.SetTrigger(triggerName);
    }
}
