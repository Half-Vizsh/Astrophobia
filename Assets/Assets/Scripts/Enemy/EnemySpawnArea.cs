using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public float minDistanceFromPlayer = 2.0f;
    public int maxSpawnAttempts = 10;

    BoxCollider2D area;
    Transform player;

    void Awake()
    {
        area = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned!");
            return;
        }

        Vector2 spawnPosition = FindValidSpawnPosition();

        int index = Random.Range(0, enemyPrefabs.Length);

        GameObject enemyPrefab = enemyPrefabs[index];

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector2 FindValidSpawnPosition()
    {
        Bounds bounds = area.bounds;

        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            Vector2 candidate = new Vector2(x, y);

            if (Vector2.Distance(candidate, player.position) >= minDistanceFromPlayer)
            {
                return candidate;
            }
        }

        return area.bounds.center;
    }

    void OnDrawGizmos()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();

        if (box == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.bounds.center, box.bounds.size);
    }
}