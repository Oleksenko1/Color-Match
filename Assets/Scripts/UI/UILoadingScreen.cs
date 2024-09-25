using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoadingScreen : MonoBehaviour
{
    public static UILoadingScreen Instance;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();

        Instance = this;
    }

    public void PlayLoadIn()
    {
        animator.SetTrigger("OnMoveIn");
    }
}
