using UnityEngine;

public class EnemyReward : MonoBehaviour
{
    EnemyWaveManager waveManager;

    [Header("Point Reward")]
    public int minPoints = 3;
    public int maxPoints = 4;

    bool rewardGiven = false;

    void Start()
    {
        waveManager = FindFirstObjectByType<EnemyWaveManager>();
    }

    void OnDestroy()
    {
        // prevent duplicate calls
        if (rewardGiven)
            return;

        rewardGiven = true;

        if (waveManager == null)
            return;

        int points = Random.Range(minPoints, maxPoints + 1);

        waveManager.AddWavePoints(points);
    }
}