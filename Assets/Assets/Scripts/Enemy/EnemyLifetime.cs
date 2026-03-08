using UnityEngine;

public class EnemyLifetime : MonoBehaviour
{
    public float minLifetime = 8f;
    public float maxLifetime = 12f;

    void Start()
    {
        float life = Random.Range(minLifetime, maxLifetime);
        Destroy(gameObject, life);
    }
}