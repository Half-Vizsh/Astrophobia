using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyWaveManager : MonoBehaviour
{
    EnemySpawnManager spawnManager;

    [Header("Wave Points")]
    public int wavePoints = 0;
    public int maxPoints = 10000;

    [Header("Special Event System")]
    public float baseSpecialChance = 0.1f;
    public float specialSpawnChance = 0f;

    public float eventCooldown = 15f;
    float eventCooldownTimer = 0f;

    string lastEventName = "None";
    int lastEventIndex = -1;

    bool waveRunning = false;

    float spawnTimer = 0f;
    float nextSpawnTime = 2f;

    float currentMinSpawn;
    float currentMaxSpawn;

    void Start()
    {
        spawnManager = GetComponent<EnemySpawnManager>();

        specialSpawnChance = baseSpecialChance;
    }

    void Update()
    {
        if (!waveRunning &&
            Keyboard.current != null &&
            Keyboard.current.lKey.wasPressedThisFrame)
        {
            StartWaveSystem();
        }

        if (!waveRunning)
            return;

        spawnTimer += Time.deltaTime;
        eventCooldownTimer += Time.deltaTime;

        if (spawnTimer >= nextSpawnTime)
        {
            spawnTimer = 0f;

            SpawnEnemyWithBias();

            UpdateSpawnInterval();
        }

        if (eventCooldownTimer >= eventCooldown)
        {
            eventCooldownTimer = 0f;

            TrySpecialSpawn();
        }

        PrintDebugInfo();
    }

    void StartWaveSystem()
    {
        waveRunning = true;
        wavePoints = 0;

        specialSpawnChance = baseSpecialChance;

        UpdateSpawnInterval();

        Debug.Log("Wave system started");
    }

    void TrySpecialSpawn()
    {
        float r = Random.value;

        if (r > specialSpawnChance)
            return;

        int eventIndex;

        do
        {
            eventIndex = Random.Range(0, 3);
        }
        while (eventIndex == lastEventIndex);

        lastEventIndex = eventIndex;

        if (eventIndex == 0)
            SpawnExplosionTrio();
        else if (eventIndex == 1)
            SpawnCardinalCrystal();
        else
            SpawnRogueSquare();

        specialSpawnChance = baseSpecialChance;

        eventCooldownTimer = 0f;
    }

    void SpawnEnemyWithBias()
    {
        int state = GetState();

        float r = Random.value;

        int enemyType = 0;

        if (state == 0)
        {
            if (r < 0.5f) enemyType = 0;
            else if (r < 0.75f) enemyType = 1;
            else enemyType = 2;
        }
        else if (state == 1)
        {
            if (r < 0.5f) enemyType = 1;
            else if (r < 0.75f) enemyType = 0;
            else enemyType = 2;
        }
        else if (state == 2)
        {
            if (r < 0.5f) enemyType = 2;
            else if (r < 0.75f) enemyType = 0;
            else enemyType = 1;
        }
        else
        {
            if (r < 0.333f) enemyType = 0;
            else if (r < 0.666f) enemyType = 1;
            else enemyType = 2;
        }

        spawnManager.SpawnEnemyType(enemyType);
    }

    void SpawnExplosionTrio()
    {
        int area = Random.Range(0, spawnManager.spawnAreas.Length);

        spawnManager.SpawnEnemyCluster(0, area, 3, 0.6f);

        lastEventName = "Explosion Trio";
    }

    void SpawnCardinalCrystal()
    {
        for (int i = 0; i < spawnManager.spawnAreas.Length; i++)
        {
            spawnManager.SpawnEnemyInArea(1, i);
        }

        lastEventName = "Cardinal Crystal";
    }

    void SpawnRogueSquare()
    {
        int area = Random.Range(0, spawnManager.spawnAreas.Length);

        GameObject enemy = Instantiate(
            spawnManager.spawnAreas[area].enemyPrefabs[2],
            spawnManager.spawnAreas[area].transform.position,
            Quaternion.identity
        );

        EnemyStats stats = enemy.GetComponent<EnemyStats>();

        if (stats != null)
        {
            stats.maxSpeed *= 3f;
        }

        enemy.AddComponent<RogueSquareTimer>();

        lastEventName = "Rogue Square";
    }

    void UpdateSpawnInterval()
    {
        int cycle = GetCycle();

        float min = 2f;
        float max = 4f;

        switch (cycle)
        {
            case 1: min = 2f; max = 4f; break;
            case 2: min = 1.9f; max = 3.8f; break;
            case 3: min = 1.8f; max = 3.6f; break;
            case 4: min = 1.7f; max = 3.4f; break;
            case 5: min = 1.6f; max = 3.2f; break;
            case 6: min = 1.5f; max = 3f; break;
            case 7: min = 1.4f; max = 2.8f; break;
            case 8: min = 1.3f; max = 2.6f; break;
            case 9: min = 1.2f; max = 2.4f; break;
            case 10: min = 1.1f; max = 2.2f; break;
            default: min = 1f; max = 2f; break;
        }

        currentMinSpawn = min;
        currentMaxSpawn = max;

        nextSpawnTime = Random.Range(min, max);
    }

    int GetCycle()
    {
        int stateIndex = wavePoints / 250;

        int cycle = (stateIndex / 4) + 1;

        return Mathf.Clamp(cycle, 1, 11);
    }

    int GetState()
    {
        int stateIndex = wavePoints / 250;

        return stateIndex % 4;
    }

    string GetBiasName()
    {
        int state = GetState();

        if (state == 0) return "Sphere";
        if (state == 1) return "Crystal";
        if (state == 2) return "Square";

        return "Balanced";
    }

    void PrintDebugInfo()
    {
        int cycle = GetCycle();
        int state = GetState() + 1;

        Debug.Log(
            "Wave (" + cycle + "-" + state + ") | " +
            "Bias: " + GetBiasName() + " | " +
            "SpawnInterval: " + currentMinSpawn.ToString("0.00") +
            " - " + currentMaxSpawn.ToString("0.00") +
            " | WavePoints: " + wavePoints +
            " | SpecialChance: " + specialSpawnChance.ToString("0.00") +
            " | Cooldown: " + eventCooldown +
            " | LastEvent: " + lastEventName
        );
    }

    public void AddWavePoints(int points)
    {
        wavePoints += points;

        if (wavePoints > maxPoints)
            wavePoints = maxPoints;

        int cycle = GetCycle();

        float bonus = 0.01f;

        if (cycle >= 4 && cycle <= 6)
            bonus = 0.02f;
        else if (cycle >= 7 && cycle <= 9)
            bonus = 0.03f;
        else if (cycle >= 10)
            bonus = 0.04f;

        specialSpawnChance += bonus;
    }
}