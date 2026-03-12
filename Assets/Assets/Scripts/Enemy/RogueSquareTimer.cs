using UnityEngine;

public class RogueSquareTimer : MonoBehaviour
{
    EnemyStats stats;

    float originalSpeed;

    void Start()
    {
        stats = GetComponent<EnemyStats>();

        if (stats != null)
            originalSpeed = stats.maxSpeed;

        Invoke("ResetSpeed", 10f);
    }

    void ResetSpeed()
    {
        if (stats != null)
            stats.maxSpeed = originalSpeed;

        Destroy(this);
    }
}