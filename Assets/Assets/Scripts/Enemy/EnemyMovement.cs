using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats stats;
    Transform player;
    Rigidbody2D rb;

    Vector2 velocity;

    [Header("Touch Bounce")]
    public float playerTouchDistance = 0.8f;
    public float playerBounceForce = 2.5f;
    public float playerBounceDuration = 0.08f;

    // sideways strength
    public float sidewaysFactor = 0.55f;

    float bounceTimer = 0f;

    // Knockback parameters (used for explosions)
    public float knockbackForce = 3.2f;
    public float knockbackControlDelay = 0.35f;
    public float knockbackDrag = 4.5f;

    float knockbackTimer = 0f;
    float originalDrag;

    [Header("Swarm Behaviour")]
    public float orbitStrength = 0.6f;
    public float orbitDistance = 1.2f;
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
        HandlePlayerTouch();

        if (bounceTimer > 0f)
        {
            bounceTimer -= Time.fixedDeltaTime;
            return;
        }

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

    void HandlePlayerTouch()
    {
        if (bounceTimer > 0f)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= playerTouchDistance)
        {
            Vector2 away = ((Vector2)transform.position - (Vector2)player.position).normalized;

            // sideways direction based on player velocity
            Vector2 playerVelocity = Vector2.zero;

            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb != null)
                playerVelocity = playerRb.linearVelocity;

            Vector2 sideways = new Vector2(-playerVelocity.y, playerVelocity.x).normalized;

            Vector2 bounceDirection = (away + sideways * sidewaysFactor).normalized;

            // instant sharp bounce
            rb.linearVelocity = bounceDirection * playerBounceForce;

            bounceTimer = playerBounceDuration;
        }
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
}