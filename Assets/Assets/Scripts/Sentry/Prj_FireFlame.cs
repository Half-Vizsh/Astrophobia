using System.Collections;
using UnityEngine;

public class Prj_FireFlame : MonoBehaviour
{
    public float Spreadingtime; 
    [SerializeField] private float PrepInterval;
    [SerializeField] private float ExitTime;
    public int damageAmount;
    Animator FlameAnimator;
    PolygonCollider2D PC2D;
    Twr_Fire_Main TwrScript;
    void Start()
    {
        TwrScript = GetComponent<Twr_Fire_Main>();
        PC2D = GetComponent<PolygonCollider2D>();
        PC2D.enabled = false;
        FlameAnimator = GetComponent<Animator>();
        StartCoroutine(FlameStart());
    }
    IEnumerator FlameStart()
    {
        FlameAnimator.SetBool("onPrep", true);
        yield return new WaitForSeconds (PrepInterval);
        FlameAnimator.SetBool("onPrep", false);
        FlameAnimator.SetBool("onSpread", true);
        PC2D.enabled = true;
        yield return new WaitForSeconds (Spreadingtime);
        PC2D.enabled = false;
        FlameAnimator.SetBool("onSpread", false);
        FlameAnimator.SetBool("onExit", true);
        yield return new WaitForSeconds (ExitTime);
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) other.GetComponent<Emy_Health>().TakingDamage(damageAmount);
    }
}
