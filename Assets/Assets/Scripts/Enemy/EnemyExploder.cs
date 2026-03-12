using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyExploder : MonoBehaviour
{
    [Header("Explosions")]
    public float triggerDistance = 1f;
    public float explosionRadius = 2.0f;
    public float explosionForce = 6f;
    public GameObject explosionPrefab;
    Transform player;
    EnemyMovement EmyMoveScript;
    bool exploded = false;
    public int DmgAmount;
    [Header("Animations")]
    Animator animator;
    Rigidbody2D RB2D;
    public float TimeBeforeExplode;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        EmyMoveScript = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
        RB2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (exploded){
            RB2D.linearVelocity = Vector2.zero;
            return;
        }
        animator.SetFloat("Vel.y",RB2D.linearVelocity.y);
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= triggerDistance)
        {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        exploded = true;
        animator.SetBool ("isExploding", true);
        EmyMoveScript.enabled = false;
        yield return new WaitForSeconds(TimeBeforeExplode);
        // Spawn explosion visual
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    //Contact Damage
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag == "Player") collision.gameObject.GetComponent<Ply_Health>().TakingDamage(DmgAmount);        
    // }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
    }
}
