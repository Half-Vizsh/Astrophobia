using Unity.VisualScripting;
using UnityEngine;

public class Prj_IceBullet : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float speed;
    public float lifeTime;
    [Header("Stats")]
    public int damageAmount;
    public float slowedSpeed;
    public float slowedAcc;
    public float slowDur;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }
    void FixedUpdate()
    {
        rb2D.AddForce(transform.up * speed);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Emy_Health>().TakingDamage(damageAmount);
            EnemyStats SpeedScript = other.GetComponent<EnemyStats>();
            StartCoroutine(SpeedScript.BeingSlowed(slowDur, slowedSpeed, slowedAcc));            
            Destroy(gameObject);            
        }
    }

}
