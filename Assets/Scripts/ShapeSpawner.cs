using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShapeSpawner : MonoBehaviour
{
    public static ShapeSpawner Instance;
    [Header("Stats")]
    [SerializeField] private float spawnDelay;
    [SerializeField] private float shapeSpeed = 2.25f;
    [Space(15)]
    [SerializeField] private List<Transform> shapeList;
    [SerializeField] private ColorsListSO colorList;
    [Header("Difficulty increase amount")]
    [SerializeField] private float spawnDelayIncrease = 0.15f;
    [SerializeField] private float shapeSpeedIncrease = 0.75f;

    [Inject] private UITimer timer;

    private bool isSpawning = true;
    private float xBoundry;
    private float yBoundry;

    private bool isSpaningRainbow = false;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        // Increasing spawn and shape speed depending on a difficulty level
        int difficultyLevel = DifficultyLevelHandler.GetLevel();
        spawnDelay -= spawnDelayIncrease * difficultyLevel;
        shapeSpeed += shapeSpeedIncrease * difficultyLevel;

        CalculateScreenBoundries();

        StartCoroutine(ShapeSpawnCoroutine());

        // Stops spawning when game is over
        timer.OnGameOver += (() => { isSpawning = false; });
    }
    IEnumerator ShapeSpawnCoroutine()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnDelay);

            Vector3 position = RandomPosition();

            ColorSO color = isSpaningRainbow? colorList.rainbowColor : colorList.list[Random.Range(0, colorList.list.Count)];
            Transform prefab = shapeList[Random.Range(0, shapeList.Count - 1)];

            ShapeBehaiviour.CreateShape(position, color, prefab, shapeSpeed);
        }
    }
    private void CalculateScreenBoundries()
    {
        Vector3 screenBoundries = UIScreenBoundries.Instance.GetBoundries();

        float xOffset = 0.4f; 
        xBoundry = screenBoundries.x - xOffset;

        float yOffset = 1f;
        yBoundry = screenBoundries.y + yOffset;
    }

    private Vector3 RandomPosition()
    {
        Vector3 rndPos = new Vector3(Random.Range(-xBoundry, xBoundry), yBoundry, 0);

        return rndPos;
    }

    public void SpawnRainbowShapes(bool b)
    {
        isSpaningRainbow = b;
    }
}
