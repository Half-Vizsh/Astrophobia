using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawnManager : MonoBehaviour
{
    EnemySpawnArea[] spawnAreas;

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

    void SpawnFromRandomArea()
    {
        if (spawnAreas.Length == 0)
        {
            Debug.LogWarning("No spawn areas found!");
            return;
        }

        int index = Random.Range(0, spawnAreas.Length);

        spawnAreas[index].SpawnEnemy();
    }
}