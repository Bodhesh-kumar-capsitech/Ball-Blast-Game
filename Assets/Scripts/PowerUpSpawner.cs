using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;  
    private float spawnInterval = 14f; 
    [SerializeField] private Vector2 spawnMinMaxX = new Vector2(-1.84f, 1.93f);
    [SerializeField] private Vector2 spawnMinMaxY = new Vector2(1.77f, 4.25f);

    private void Start()
    {
        StartCoroutine(SpawnPowerUpRoutine());
    }

    private IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp()
    {
        if (powerUpPrefab == null) return;

        //Random position within bounds
        float x = Random.Range(spawnMinMaxX.x, spawnMinMaxX.y);
        float y = Random.Range(spawnMinMaxY.x, spawnMinMaxY.y);
        Vector2 spawnPos = new Vector2(x, y);

        GameObject pu = Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);

    }
}
