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
    [SerializeField] Ply_Health ply_Health;
    public InputAction DodgeButton;
    Vector2 facing = new Vector2 (0,1);
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
        Vector2 position = rb2D.position + speed * Time.deltaTime * move;
        rb2D.MovePosition(position);
    }
}
