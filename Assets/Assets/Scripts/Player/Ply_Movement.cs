using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ply_Movement : MonoBehaviour
{
    [Header("Movement")]
    public InputAction MoveButton;
    public float speed;
    Rigidbody2D rb2D;
    public Vector2 move;

    [Header("Dodge")]
    public InputAction DodgeButton;
    [SerializeField] Ply_Health ply_Health;
    [SerializeField] float dodgeDur;
    [SerializeField] float dodgeCD;
    [SerializeField] float DodgeSpeed;
    public Vector2 facing = new Vector2 (0,1);
    public bool isDodging = false;
    bool canDodge = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveButton.Enable();
        DodgeButton.Enable();
        rb2D = GetComponent<Rigidbody2D>();
        ply_Health = GetComponent<Ply_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveButton.ReadValue<Vector2>();
        if (!Mathf.Approximately(move.x, 0.0f)|| !Mathf.Approximately(move.y,0.0f))
        {
            facing.Set (move.x, move.y);
            facing.Normalize();
        }
    }
    
    void FixedUpdate()
    {
        var dodgeCek = DodgeButton.ReadValue<float>();
        if (Mathf.Approximately(dodgeCek, 1f)&&canDodge)
        {
            StartCoroutine(Dodge());
            return;
        }
        if (!isDodging)rb2D.linearVelocity = move.normalized*speed;
    }

    private IEnumerator Dodge()
    {
        isDodging = true;
        canDodge = false;
        var timeElapse = 0f;
        while (timeElapse < dodgeDur)
        {
            var t = timeElapse/dodgeDur;
            float gradualSpeed = 1f-t;
            rb2D.linearVelocity = facing.normalized*DodgeSpeed*gradualSpeed;
            timeElapse += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        isDodging = false;
        yield return new WaitForSeconds(dodgeCD);
        canDodge = true;
    }
}
