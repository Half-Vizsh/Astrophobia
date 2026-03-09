using UnityEngine;

public class EnemyExploder : MonoBehaviour
{
    public float triggerDistance = 0.9f;

    public float explosionRadius = 2.0f;
    public float explosionForce = 6f;

    public LayerMask affectedLayers;

    Transform player;

    bool exploded = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (exploded) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= triggerDistance)
        {
            Debug.Log("EXPLODING");
            Explode();
        }
    }

    void Explode()
    {
        exploded = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            explosionRadius,
            affectedLayers
        );

        foreach (Collider2D hit in hits)
        {
            Vector2 dir = (hit.transform.position - transform.position).normalized;

            // If object has EnemyMovement, use its knockback system
            EnemyMovement enemy = hit.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.ApplyKnockback(transform.position);
                continue;
            }

            // If object has Rigidbody (like player), apply force
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(dir * explosionForce, ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}