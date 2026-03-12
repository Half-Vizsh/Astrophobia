using UnityEngine;

public class EnemyExploder : MonoBehaviour
{
    public float triggerDistance = 1f;

    public float explosionRadius = 2.0f;
    public float explosionForce = 6f;

    public GameObject explosionPrefab;

    Transform player;
    Ply_Movement playerMovement;

    bool exploded = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = player.GetComponent<Ply_Movement>();
    }

    void Update()
    {
        if (exploded) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= triggerDistance)
        {
            Explode();
        }
    }

    void Explode()
    {
        exploded = true;

        // Spawn explosion visual
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        float playerDistance = Vector2.Distance(transform.position, player.position);

        if (playerDistance <= explosionRadius)
        {
            // Snapshot player position
            Vector2 snapshotPlayerPos = player.position;

            // Compute knockback direction
            Vector2 knockbackDir =
                (snapshotPlayerPos - (Vector2)transform.position).normalized;

            if (playerMovement != null)
            {
                playerMovement.ApplyExplosionKnockback(knockbackDir * explosionForce);
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
