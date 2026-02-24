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
    Vector2 move;

    [Header("Dodge")]
    public InputAction DodgeButton;
    [SerializeField] Ply_Health ply_Health;
    [SerializeField] float dodgeDur;
    [SerializeField] float DodgeSpeed;
    Vector2 facing = new Vector2 (0,1);
    bool isDodging = false;
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
        rb2D.linearVelocity = new Vector2 (move.x*speed,move.y*speed);
        // Vector2 position = rb2D.position + speed * Time.deltaTime * move;
        // rb2D.MovePosition(position);
    }

    private IEnumerator Dodge()
    {
        isDodging = true;
        canDodge = false;
        rb2D.linearVelocity = new Vector2 (facing.x*DodgeSpeed,facing.y*DodgeSpeed);
        yield return new WaitForSeconds(dodgeDur);
        isDodging = false;
        canDodge = true;
    }
}
