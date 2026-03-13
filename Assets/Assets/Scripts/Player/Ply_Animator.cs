using System.Collections;
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
    public Ply_Health HealthScript;
    public SpriteRenderer sr;
    bool isDead;
    [SerializeField] float DeathTime;
    void Start()
    {
        MoveScript = GetComponent<Ply_Movement>();
        BuildScript = GetComponent<Ply_Buildmanager>();
        HealthScript = GetComponent<Ply_Health>();
        animator = GetComponentInChildren<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        isDead = false;
    }

    // (I think I should've used state machine :/)
    void Update()
    {
        if (isDead) return;

        if (MoveScript.facing.x<0)sr.flipX = true;
        else sr.flipX = false;
        bool Building = BuildScript.isBuildingSmt; 
        bool Dodging = MoveScript.isDodging;
        bool Moving = MoveScript.move.sqrMagnitude >= 0.01f;
        bool Dying = HealthScript.Health <= 0; 
        bool GettingDamage = HealthScript.spriteWhite;

        if (Dying)
        {
            //Handle Dying
            rb2D.linearVelocity = Vector2.zero;
            MoveScript.enabled = false;
            isDead = true;
            animator.SetBool("isDying", true);
            Destroy(gameObject, DeathTime);
            return;
        }
        animator.SetBool("isDamage", GettingDamage);
        if (Building)
        {
            rb2D.linearVelocity = Vector2.zero;
            MoveScript.enabled = false;
            MoveScript.move = Vector2.zero;
            animator.SetBool("isBuilding", true);
            animator.SetBool("isMoving", false);
            animator.SetBool("isDashing", false);
        }else if (Moving)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isDashing", false);
            animator.SetBool("isBuilding", false);
        } else if (Dodging) {
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
