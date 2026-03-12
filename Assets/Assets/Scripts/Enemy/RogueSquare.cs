using UnityEngine;

public class RogueSquare : MonoBehaviour
{
    EnemyStats stats;

    public float rogueSpeedMultiplier = 3f;
    public float rogueDuration = 10f;

    float originalSpeed;

    void Start()
    {
        stats = GetComponent<EnemyStats>();

        if (stats == null)
            return;

        originalSpeed = stats.maxSpeed;

        stats.maxSpeed *= rogueSpeedMultiplier;

        Invoke("ResetSpeed", rogueDuration);
    }

    void ResetSpeed()
    {
        if (stats != null)
            stats.maxSpeed = originalSpeed;
    }
}