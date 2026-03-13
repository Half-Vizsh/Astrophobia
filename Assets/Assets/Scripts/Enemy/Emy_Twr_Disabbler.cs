using UnityEngine;

public class Emy_Twr_Disabbler : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetFloat("Vel.y",rb2d.linearVelocity.y);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Ply_Health PlyHealthScript = collision.gameObject.GetComponent<Ply_Health>();
        PlyHealthScript.TakingDamage(1, transform.position);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Twr_Behaviour TwrScript = collision.GetComponentInChildren<Twr_Behaviour>();
        if (TwrScript!=null) {
            TwrScript.DisableTower();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        Twr_Behaviour TwrScript = collision.GetComponentInChildren<Twr_Behaviour>();
        if (TwrScript!=null) TwrScript.EnableTower();
    }
}
