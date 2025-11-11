using UnityEngine;
using System.Collections;

public class BallSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] balls;
    [SerializeField] private float minX = -1.84f;
    [SerializeField] private float maxX = 1.93f;
    [SerializeField] private float minY = 1.77f;
    [SerializeField] private float maxY = 4.25f;
    private float spawnDelay = 2.0f;
    private float delayTime;
    private StartGame level;

    private void Awake()
    {
        level = StartGame.instance;
    }
    void Start()
    {
        //Start random spawning
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        if (level.isBeginner == true)
        {
            delayTime = 2.0f;
        }
        else if (level.isIntermediate == true)
        {
            delayTime = 1.2f;
        }

    }
    void SpawnBall()
    {
        //pick random ball
        GameObject randomBall = balls[Random.Range(0, balls.Length)];

        //Generate a random position
        float randomXLeft = Random.Range(-4.34f,minX);
        float randomXRight = Random.Range(maxX, 4.42f);

        float randomX = (Random.value) < 0.5 ? randomXLeft : randomXRight;

        float randomY = Random.Range(minY, maxY);
        Vector2 spawnPos = new Vector2(randomX, randomY);

        //Spawn the ball
        Instantiate(randomBall, spawnPos, Quaternion.identity);
    }

    
    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(spawnDelay);
        while (true)
        {
            SpawnBall();
            yield return new WaitForSeconds(delayTime);
        }
    }
    
}
