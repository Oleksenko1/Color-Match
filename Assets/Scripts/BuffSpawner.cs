using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuffSpawner : MonoBehaviour
{
    [SerializeField] private int startChance = 10;
    [SerializeField] private int updateDelay = 10;
    [SerializeField] private int chanceIncrease = 10;
    [Space(15)]
    [SerializeField] private List<GameObject> buffList;

    [Inject]
    private PlayerBehaviour player;
    [Inject]
    private UITimer timer;

    private int currentChance;
    private Vector3 worldCorners;

    private float xBoundry;
    private float yBoundry;

    private bool isSpawning = true;
    private void Start()
    {
        StartCoroutine(BuffCoroutine());

        currentChance = startChance;

        CalclateScreenBorders();

        timer.OnGameOver += StopSpawning;
    }
    private void CalclateScreenBorders()
    {
        worldCorners = UIScreenBoundries.Instance.GetBoundries();

        float xOffset = 0.4f;
        xBoundry = worldCorners.x - xOffset;

        float yOffset = 1f;
        yBoundry = worldCorners.y + yOffset;
    }
    IEnumerator BuffCoroutine()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(updateDelay);

            int randInt = Random.Range(0, 100);

            if (randInt <= currentChance && player.currentBuff == null && isSpawning)
            {
                currentChance = chanceIncrease;

                SpawnBuff();
            }

            currentChance += chanceIncrease;
        }
    }

    private void SpawnBuff()
    {
        Debug.Log("Buff spawned");

        GameObject randBuf = buffList[Random.Range(0, buffList.Count)];

        Vector3 randPos = new Vector3(Random.Range(-xBoundry, xBoundry), yBoundry);

        Instantiate(randBuf, randPos, Quaternion.identity);
    }

    private void StopSpawning()
    {
        StopCoroutine(BuffCoroutine());
        isSpawning = false;
    }
}
