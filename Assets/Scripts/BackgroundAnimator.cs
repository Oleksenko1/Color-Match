using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimator : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float moveSpeed;

    private float offset = 0;
    private void Update()
    {
        offset -= Time.deltaTime * moveSpeed * 0.01f;
        material.mainTextureOffset = new Vector2(0, offset);
    }
}
