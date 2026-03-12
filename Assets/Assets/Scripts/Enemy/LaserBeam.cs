using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [Header("Beam Settings")]
    public float duration = 0.25f;
    public int LaserDMG;

    Vector2 direction;

    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized;

        // rotate sprite so its UP axis points to the target
        transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
    }

    void Start()
    {
        Destroy(gameObject, duration);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Ply_Health>().TakingDamage(LaserDMG);
        }
    }
}