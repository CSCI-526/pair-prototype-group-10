using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public static int enemyCount = 0;

    void Start()
    {
        enemyCount = 0;
        InvokeRepeating("SpawnEnemy", 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(6, 1, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemyCount++;
    }
}
