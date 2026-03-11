using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class Ply_Animator : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2D;
    public Ply_Movement MoveScript;
    public Ply_Buildmanager BuildScript;
    public SpriteRenderer sr;
    void Start()
    {
        MoveScript = GetComponent<Ply_Movement>();
        BuildScript = GetComponent<Ply_Buildmanager>();
        animator = GetComponentInChildren<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // (I think I should've used state machine :/)
    void Update()
    {
        if (MoveScript.facing.x<0)sr.flipX = true;
        else sr.flipX = false;
        bool Building = BuildScript.isBuildingSmt; 
        bool Dodging = MoveScript.isDodging;

        if (Building)
        {
            rb2D.linearVelocity = Vector2.zero;
            MoveScript.enabled = false;
            MoveScript.move = Vector2.zero;
            animator.SetBool("isBuilding", true);
            animator.SetBool("isMoving", false);
            animator.SetBool("isDashing", false);
        }else if (!Dodging&&!Building&&MoveScript.move != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isDashing", false);
            animator.SetBool("isBuilding", false);
        } else if (Dodging&&!Building) {
            animator.SetBool("isDashing", true);
            animator.SetBool("isMoving", false);
            animator.SetBool("isBuilding", false);
        }
        else {
            MoveScript.enabled = true;
            animator.SetBool("isMoving", false);
            animator.SetBool("isDashing", false);
            animator.SetBool("isBuilding", false);
        }
    }
}
