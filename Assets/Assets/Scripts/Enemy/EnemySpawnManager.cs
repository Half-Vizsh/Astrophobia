using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawnManager : MonoBehaviour
{
    public EnemySpawnArea[] spawnAreas;

    void Start()
    {
        spawnAreas = FindObjectsByType<EnemySpawnArea>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {
            SpawnFromRandomArea();
        }
    }

    public void SpawnFromRandomArea()
    {
        if (spawnAreas.Length == 0)
            return;

        int areaIndex = Random.Range(0, spawnAreas.Length);

        spawnAreas[areaIndex].SpawnEnemy();
    }

    public void SpawnEnemyType(int enemyType)
    {
        if (spawnAreas.Length == 0)
            return;

        int areaIndex = Random.Range(0, spawnAreas.Length);

        spawnAreas[areaIndex].SpawnEnemy(enemyType);
    }

    public void SpawnEnemyInArea(int enemyType, int areaIndex)
    {
        if (areaIndex < 0 || areaIndex >= spawnAreas.Length)
            return;

        spawnAreas[areaIndex].SpawnEnemy(enemyType);
    }

    public void SpawnEnemyCluster(int enemyType, int areaIndex, int count, float spacing)
    {
        if (areaIndex < 0 || areaIndex >= spawnAreas.Length)
            return;

        Vector2 center = spawnAreas[areaIndex].transform.position;

        for (int i = 0; i < count; i++)
        {
            Vector2 offset = Random.insideUnitCircle * spacing;

            Instantiate(
                spawnAreas[areaIndex].enemyPrefabs[enemyType],
                center + offset,
                Quaternion.identity
            );
        }
    }
}