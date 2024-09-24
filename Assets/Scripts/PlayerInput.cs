using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    [Inject]
    private PlayerBehaviour playerBehaviour;
    [Inject]
    private UITimer timer;

    private Camera mainCamera;
    private Vector3 startPosition;
    private Touch touch;
    private bool isPlaying = true;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Start()
    {
        // Stops getting movement input when game is over
        timer.OnGameOver += (() => { isPlaying = false; });
    }
    private void Update()
    {
        if (Input.touches.Length != 0 && isPlaying)
        {
            touch = Input.touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Sets start position of finger
                    startPosition = mainCamera.ScreenToWorldPoint(touch.position);
                    break;

                case TouchPhase.Moved:
                    float targetPosition = startPosition.x - mainCamera.ScreenToWorldPoint(touch.position).x;

                    playerBehaviour.SetTargetPosition(targetPosition);

                    startPosition.x -= targetPosition;
                    break;
            }
        }
    }
}
