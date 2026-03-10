using UnityEngine;
using UnityEngine.Rendering;

public class Ply_Animator : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2D;
    public Ply_Movement MoveScript;
    public SpriteRenderer sr;
    void Start()
    {
        MoveScript = GetComponent<Ply_Movement>();
        animator = GetComponentInChildren<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveScript.facing.x<0)sr.flipX = true;
        else sr.flipX = false;
        if (!MoveScript.isDodging&&MoveScript.move != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isDashing", false);
        } else if (MoveScript.isDodging) {
            animator.SetBool("isDashing", true);
            animator.SetBool("isMoving", false);
        }
        else {
            animator.SetBool("isMoving", false);
            animator.SetBool("isDashing", false);
        }
    }
}
