using Unity.VisualScripting;
using UnityEngine;

public class Prj_IceBullet : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float speed;
    public float lifeTime;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    public void FixedUpdate()
    {
        rb2D.AddForce(transform.up * speed);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dummy")
        {
            other.GetComponent<Dummy_TakingDamage>().TakingDamage(1);
            //other.GetComponent<Rigidbody2D>().AddForce(transform.up*PushPower);
            // Apply CC here, probably need enemy
        }
        Destroy(gameObject);
    }

}
