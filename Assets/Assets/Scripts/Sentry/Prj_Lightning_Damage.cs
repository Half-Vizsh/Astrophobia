using UnityEngine;

public class Prj_Lightning_Damage : MonoBehaviour
{
   public int damageAmount;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) collision.GetComponent<Emy_Health>().TakingDamage(damageAmount);
    }
}
