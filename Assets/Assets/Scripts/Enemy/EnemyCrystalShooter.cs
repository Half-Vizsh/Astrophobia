using UnityEngine;

public class EnemyCrystalShooter : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRange = 6f;

    [Header("Timing")]
    public float aimTime = 0.6f;
    public float cooldownTime = 4f;

    [Header("Movement")]
    public float stopDeceleration = 10f;

    [Header("Laser")]
    public GameObject laserPrefab;
    public Transform firePoint;

    [Header("Aim Behaviour")]
    public float predictionStrength = 0.6f;   // how strong the lead is
    public float randomSpreadAngle = 3f;      // random aim offset
    public float predictionChance = 0.7f;     // chance to use prediction

    Transform player;
    Rigidbody2D playerRb;
    Rigidbody2D rb;
    EnemyMovement movement;
    Animator animator;

    float stateTimer = 0f;

    enum State
    {
        Pursue,
        Aim,
        Cooldown
    }

    State currentState = State.Pursue;

    Vector2 snapshotTarget;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerRb = player.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;
        float distance = Vector2.Distance(transform.position, player.position);
        switch (currentState)
        {
            case State.Pursue:
                if (distance <= detectionRange)
                {
                    currentState = State.Aim;
                    stateTimer = aimTime;

                    snapshotTarget = player.position;
                }
                break;
            case State.Aim:
                SmoothStop();
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0)
                {
                    animator.SetBool("isAttacking", true);
                    // FireLaser();

                    // currentState = State.Cooldown;
                    // stateTimer = cooldownTime;
                }
                break;
            case State.Cooldown:
                animator.SetBool("isAttacking", false);
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0)
                {
                    currentState = State.Pursue;
                }
                break;
        }
    }
    void SmoothStop()
    {
        rb.linearVelocity = Vector2.Lerp(
            rb.linearVelocity,
            Vector2.zero,
            stopDeceleration * Time.deltaTime
        );
    }

    void FireLaser()
    {
        Vector2 firePos = firePoint.position;
        // Base direction from snapshot
        Vector2 baseDirection = (snapshotTarget - firePos).normalized;
        Vector2 finalDirection = baseDirection;
        // Occasionally use predictive aiming
        if (playerRb != null && Random.value < predictionChance)
        {
            Vector2 playerVelocity = playerRb.linearVelocity;
            Vector2 predictedOffset =
                playerVelocity * predictionStrength;
            Vector2 predictedTarget =
                snapshotTarget + predictedOffset;
            finalDirection =
                (predictedTarget - firePos).normalized;
        }
        // Add randomized spread
        float randomAngle =
            Random.Range(-randomSpreadAngle, randomSpreadAngle);
        finalDirection =
            Quaternion.Euler(0, 0, randomAngle) * finalDirection;
        GameObject laser = Instantiate(
            laserPrefab,
            firePos,
            Quaternion.identity
        );
        LaserBeam beam = laser.GetComponent<LaserBeam>();
        beam.Initialize(finalDirection);
        currentState = State.Cooldown;
        stateTimer = cooldownTime;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") collision.gameObject.GetComponent<Ply_Health>().TakingDamage(1, transform.position);
    }
}
