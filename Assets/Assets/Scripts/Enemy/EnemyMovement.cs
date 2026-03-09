using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats stats;
    Transform player;
    Rigidbody2D rb;

    Vector2 velocity;

    // Knockback parameters
    public float knockbackForce = 3.2f;
    public float knockbackControlDelay = 0.35f;
    public float knockbackDrag = 4.5f;

    float knockbackTimer = 0f;
    float originalDrag;

    // Swarm behavior parameters
    public float orbitStrength = 1.2f;
    public float orbitDistance = 1.5f;
    public float arrivalDistance = 0.8f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<EnemyStats>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalDrag = rb.linearDamping;
    }

    void FixedUpdate()
    {
        if (knockbackTimer > 0f)
        {
            knockbackTimer -= Time.fixedDeltaTime;

            float controlFactor = 1f - (knockbackTimer / knockbackControlDelay);
            controlFactor = Mathf.Clamp01(controlFactor);

            Vector2 desired = SeekPlayer() + Separation() + OrbitPlayer();
            desired = desired.normalized * stats.maxSpeed;

            Vector2 blendedVelocity =
                Vector2.Lerp(rb.linearVelocity, desired,
                controlFactor * stats.acceleration * Time.fixedDeltaTime);

            rb.linearVelocity = blendedVelocity;

            if (knockbackTimer <= 0f)
                rb.linearDamping = originalDrag;

            return;
        }

        Vector2 target = SeekPlayer() + Separation() + OrbitPlayer();

        target = target.normalized * stats.maxSpeed;

        velocity = Vector2.MoveTowards(
            rb.linearVelocity,
            target,
            stats.acceleration * Time.fixedDeltaTime
        );

        rb.linearVelocity = velocity;
    }

    Vector2 SeekPlayer()
    {
        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        if (distance < arrivalDistance)
        {
            float slowdown = distance / arrivalDistance;
            return toPlayer.normalized * slowdown;
        }

        return toPlayer.normalized;
    }

    Vector2 OrbitPlayer()
    {
        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        if (distance < orbitDistance)
        {
            Vector2 tangent = new Vector2(-toPlayer.y, toPlayer.x).normalized;
            return tangent * orbitStrength;
        }

        return Vector2.zero;
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

            if (away.magnitude > 0)
                force += away.normalized / away.magnitude;
        }

        return force * stats.separationStrength;
    }

    public void ApplyKnockback(Vector2 sourcePosition)
    {
        Vector2 dir = ((Vector2)transform.position - sourcePosition).normalized;

        rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);

        rb.linearDamping = knockbackDrag;

        knockbackTimer = knockbackControlDelay;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyKnockback(collision.transform.position);
        }
    }
}