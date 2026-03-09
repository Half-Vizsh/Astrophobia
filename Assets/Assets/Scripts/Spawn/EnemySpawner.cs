using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;

    public float spawnRadius = 4f;
    public float spawnInterval = 1f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    void SpawnEnemy()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector2 spawnPos = (Vector2)player.position + direction * spawnRadius;

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}