using Unity.VisualScripting;
using UnityEngine;

public class Prj_IceBullet : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float speed;
    public float lifeTime;
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
        if (other.tag == "Dummy")
        {
            other.GetComponent<Dummy_TakingDamage>().TakingDamage(1);
            Destroy(gameObject);
            //other.GetComponent<Rigidbody2D>().AddForce(transform.up*PushPower);
            // Apply CC here, probably need enemy
        }
    }

}
