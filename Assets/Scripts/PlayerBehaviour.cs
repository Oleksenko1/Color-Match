using System.Collections;
using UnityEngine;
using System;
using Zenject;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public event Action<bool> OnColorCollect;
    public event Action OnColorChangeCaution;
    public event Action OnColorChanged;

    [SerializeField] private float colorChangeDelay = 5f;
    [SerializeField] private ColorsListSO colorsList;
    [Space(10)]
    [SerializeField] private RectTransform canvas;
    [SerializeField] private float borderOffset = -0.4f;
    [Header("SFX")]
    [SerializeField] private AudioClip ColorChangeSFX;
    [SerializeField] private AudioClip CorrectCollectSFX;
    [SerializeField] private AudioClip WrongCollectSFX;
    [Header("VFX")]
    [SerializeField] private GameObject colorChangeVFX;

    private SpriteRenderer sprite;

    [Inject] private UITimer timer;

    private ColorSO currentColor;

    private float colorChangeCautionTime = 3f;
    private Vector3 targetPosition;
    private Rigidbody2D rb;
    private bool isPlaying = true;
    private ColorSO newColor;
    private float border;
    private void Awake()
    {
        Vector3[] worldCorners = new Vector3[4];
        canvas.GetWorldCorners(worldCorners);
        border = worldCorners[2].x + borderOffset;

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        targetPosition = transform.position;

        // Sets random color type on awake
        currentColor = newColor = colorsList.list[UnityEngine.Random.Range(0, colorsList.list.Count - 1)];
        sprite.color = currentColor.color;
    }
    private void Start()
    {
        StartCoroutine(ColorChangeCoroutine());

        // Stops detecting collisions when game is over
        timer.OnGameOver += (() => { isPlaying = false; });
    }
    private void Update()
    {
        if(transform.position != targetPosition)
        {
            rb.MovePosition(targetPosition);
        }
    }
    public void SetTargetPosition(float xPosition)
    {
        targetPosition.x -= xPosition;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -border, border);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isPlaying) return;

        // If color is the same as the bucket - collect shape
        if(collision.GetComponent<ShapeBehaiviour>().GetColorSO() == currentColor)
        {
            OnColorCollect?.Invoke(true);
            SoundsHandler.PlaySFX(CorrectCollectSFX, 1f);
        }
        else
        {
            OnColorCollect?.Invoke(false);
            SoundsHandler.PlaySFX(WrongCollectSFX, 1f);
        }

        Destroy(collision.gameObject);
    }

    IEnumerator ColorChangeCoroutine()
    {
        while (isPlaying)
        {
            yield return new WaitForSeconds(colorChangeDelay - colorChangeCautionTime);
            OnColorChangeCaution?.Invoke();

            yield return new WaitForSeconds(colorChangeCautionTime);
            int index;

            if (isPlaying)
            {
                // Generetaing new color. If it's the same color - generate again
                do
                {
                    index = UnityEngine.Random.Range(0, colorsList.list.Count - 1);
                    newColor = colorsList.list[index];
                } while (currentColor == newColor);

                Instantiate(colorChangeVFX, transform.position + Vector3.back, colorChangeVFX.transform.rotation);

                currentColor = newColor;
                sprite.color = currentColor.color;

                SoundsHandler.PlaySFX(ColorChangeSFX, 1f);
                OnColorChanged?.Invoke();
            }
        }
    }

    public ColorSO GetColor()
    {
        return currentColor;
    }
}
