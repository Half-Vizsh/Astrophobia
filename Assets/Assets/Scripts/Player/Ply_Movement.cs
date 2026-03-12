using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ply_Movement : MonoBehaviour
{
    [Header("Movement")]
    public InputAction MoveButton;
    public float speed;

    Rigidbody2D rb2D;
    Vector2 move;

    [Header("Dodge")]
    public InputAction DodgeButton;
    [SerializeField] Ply_Health ply_Health;
    [SerializeField] float dodgeDur;
    [SerializeField] float dodgeCD;
    [SerializeField] float DodgeSpeed;

    Vector2 facing = new Vector2(0, 1);

    bool isDodging = false;
    bool canDodge = true;

    float controlMultiplier = 1f;

    void Start()
    {
        MoveButton.Enable();
        DodgeButton.Enable();

        rb2D = GetComponent<Rigidbody2D>();
        ply_Health = GetComponent<Ply_Health>();
    }

    void Update()
    {
        move = MoveButton.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            facing.Set(move.x, move.y);
            facing.Normalize();
        }
    }

    void FixedUpdate()
    {
        var dodgeCek = DodgeButton.ReadValue<float>();

        if (Mathf.Approximately(dodgeCek, 1f) && canDodge)
        {
            StartCoroutine(Dodge());
            return;
        }

        if (!isDodging)
        {
            rb2D.linearVelocity = move.normalized * speed * controlMultiplier;
        }
    }

    private IEnumerator Dodge()
    {
        isDodging = true;
        canDodge = false;

        var timeElapse = 0f;

        while (timeElapse < dodgeDur)
        {
            var t = timeElapse / dodgeDur;

            float gradualSpeed = 1f - t;

            rb2D.linearVelocity = facing.normalized * DodgeSpeed * gradualSpeed;

            timeElapse += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        isDodging = false;

        yield return new WaitForSeconds(dodgeCD);

        canDodge = true;
    }

    public void ApplyExplosionKnockback(Vector2 force)
    {
        StartCoroutine(ExplosionKnockbackRoutine(force));
    }

    IEnumerator ExplosionKnockbackRoutine(Vector2 force)
    {
        // Apply impulse knockback
        rb2D.AddForce(force, ForceMode2D.Impulse);

        // Reduce control
        controlMultiplier = 0.2f;

        float recoveryTime = 0.4f;
        float t = 0f;

        while (t < recoveryTime)
        {
            controlMultiplier = Mathf.Lerp(0.2f, 1f, t / recoveryTime);

            t += Time.deltaTime;

            yield return null;
        }

        controlMultiplier = 1f;
    }
}