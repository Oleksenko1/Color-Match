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
    [SerializeField] private float borderOffset = -0.4f;
    [Header("SFX")]
    [SerializeField] private AudioClip ColorChangeSFX;
    [SerializeField] private AudioClip CorrectCollectSFX;
    [SerializeField] private AudioClip WrongCollectSFX;
    [Header("VFX")]
    [SerializeField] private GameObject colorChangeVFX;
    [Header("Other")]
    [SerializeField] public BuffAtributesHolder buffAtributesHolder;

    private SpriteRenderer sprite;

    [Inject] private UITimer timer;

    private ColorSO currentColor;

    private float colorChangeCautionTime = 3f;
    private Vector3 targetPosition;
    private Rigidbody2D rb;
    private bool isPlaying = true;
    private ColorSO newColor;
    private float border;

    public AbstractBuff currentBuff = null;
    private void Awake()
    {
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

        Vector3 worldCorners = UIScreenBoundries.Instance.GetBoundries();
        border = worldCorners.x + borderOffset;
    }
    private void Update()
    {
        if(transform.position != targetPosition)
        {
            rb.MovePosition(targetPosition);
        }

        if(currentBuff != null)
        {
            currentBuff.UpdateBuff(this);
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

        if (collision.CompareTag("Shapes"))
        {
            ColorSO shapeColor = collision.GetComponent<ShapeBehaiviour>().GetColorSO();
            // If color is the same as the bucket - collect shape
            if (shapeColor == currentColor || shapeColor.nameString == "Rainbow")
            {
                OnColorCollect?.Invoke(true);
                SoundsHandler.PlaySFX(CorrectCollectSFX, 1f);
            }
            else
            {
                OnColorCollect?.Invoke(false);
                SoundsHandler.PlaySFX(WrongCollectSFX, 1f);
            }
        }
        else if(collision.CompareTag("Buff"))
        {
            currentBuff = collision.GetComponent<BuffHolder>().GetBuff();
            currentBuff.PickUp(this);

            Destroy(collision.gameObject);
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
    public Transform GetTransform()
    {
        return transform;
    }
}
