using Unity.VisualScripting;
using UnityEngine;

public class WindBullet : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float speed;
    public void FixedUpdate()
    {
        rb2D.AddForce(transform.up * speed);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dummy")
        {
            other.GetComponent<Dummy_TakingDamage>().TakingDamage(1);
        }
        Destroy(gameObject);
    }

}
