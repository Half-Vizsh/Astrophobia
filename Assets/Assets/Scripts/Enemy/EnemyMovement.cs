using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats stats;
    Transform player;
    Rigidbody2D rb;
    Rigidbody2D playerRb;

    Vector2 velocity;

    [Header("Touch Bounce")]
    public float playerTouchDistance = 0.8f;
    public float playerBounceForce = 4.5f;
    public float playerBounceDuration = 0.08f;

    // Player must actually be moving to cause knockback
    public float minimumPlayerSpeed = 0.15f;

    // sideways strength
    public float sidewaysFactor = 0.45f;

    float bounceTimer = 0f;

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
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        HandlePlayerTouch();

        if (bounceTimer > 0f)
        {
            bounceTimer -= Time.fixedDeltaTime;
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

        if (distance > playerTouchDistance)
            return;

        if (playerRb == null)
            return;

        Vector2 playerVelocity = playerRb.linearVelocity;

        float playerSpeed = playerVelocity.magnitude;

        // If player isn't moving, do NOT push enemy
        if (playerSpeed < minimumPlayerSpeed)
            return;

        // Direction player is moving
        Vector2 moveDir = playerVelocity.normalized;

        // sideways deflection
        Vector2 sideways = new Vector2(-moveDir.y, moveDir.x);

        Vector2 bounceDirection =
            (moveDir + sideways * sidewaysFactor).normalized;

        rb.linearVelocity = bounceDirection * playerBounceForce;

        bounceTimer = playerBounceDuration;
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
}