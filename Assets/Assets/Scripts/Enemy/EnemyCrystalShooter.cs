using UnityEngine;

public class EnemyCrystalShooter : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRange = 6f;

    [Header("Timing")]
    public float aimTime = 0.4f;
    public float cooldownTime = 1.5f;

    [Header("Movement")]
    public float stopDeceleration = 8f;

    [Header("Laser")]
    public GameObject laserPrefab;
    public Transform firePoint;

    Transform player;
    Rigidbody2D rb;
    EnemyMovement movement;

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

        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Pursue:

                if (distance <= detectionRange)
                {
                    currentState = State.Aim;
                    stateTimer = aimTime;

                    // snapshot player position
                    snapshotTarget = player.position;
                }

                break;

            case State.Aim:

                SmoothStop();

                stateTimer -= Time.deltaTime;

                if (stateTimer <= 0)
                {
                    FireLaser();

                    currentState = State.Cooldown;
                    stateTimer = cooldownTime;
                }

                break;

            case State.Cooldown:

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
        Vector2 direction = (snapshotTarget - (Vector2)firePoint.position).normalized;

        GameObject laser = Instantiate(
            laserPrefab,
            firePoint.position,
            Quaternion.identity
        );

        LaserBeam beam = laser.GetComponent<LaserBeam>();
        beam.Initialize(direction);
    }
}