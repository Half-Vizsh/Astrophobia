using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats stats;
    Transform player;
    Rigidbody2D rb;

    Vector2 velocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<EnemyStats>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Vector2 desired = SeekPlayer() + Separation();
        desired = desired.normalized * stats.maxSpeed;

        velocity = Vector2.MoveTowards(
            velocity,
            desired,
            stats.acceleration * Time.fixedDeltaTime
        );

        rb.linearVelocity = velocity;
    }

    Vector2 SeekPlayer()
    {
        Vector2 dir = player.position - transform.position;
        return dir.normalized;
    }

    Vector2 Separation()
    {
        Collider2D[] neighbors = Physics2D.OverlapCircleAll(
            transform.position,
            stats.separationRadius
        );

        Vector2 force = Vector2.zero;

        foreach (Collider2D c in neighbors)
        {
            if (c.gameObject == gameObject)
                continue;

            if (!c.CompareTag("Enemy"))
                continue;

            Vector2 away = transform.position - c.transform.position;
            force += away.normalized / away.magnitude;
        }

        return force * stats.separationStrength;
    }
}