using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffObjectMovement : MonoBehaviour
{
    private float moveSpeed = 1.5f;
    private void Awake()
    {
        int difficultyLevel = DifficultyLevelHandler.GetLevel();
        moveSpeed += 0.75f * difficultyLevel;
    }
    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);

        // Destroys shape if it's out of bounds
        if (transform.position.y < -5.5f) Destroy(gameObject);
    }
}
