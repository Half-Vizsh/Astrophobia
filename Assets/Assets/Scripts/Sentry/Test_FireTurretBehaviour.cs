using System.Collections;
using UnityEngine;

public class Test_FireTurretBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool enemyDetected;
    public int SentryDamage;
    public float cooldown;
    public float ThrowerDur;
    private float currentCD;
    [SerializeField] ParticleSystem fireFX;
    [SerializeField] PolygonCollider2D Colli;
    void Start()
    {
        fireFX.Stop();
    }
    void Update()
    {
        if (currentCD<=0) StartCoroutine(FlameThrowing());
        else currentCD -=Time.deltaTime;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Dummy")
        {
            Dummy_TakingDamage EnemyScript = collision.gameObject.GetComponent<Dummy_TakingDamage>();
            EnemyScript.TakingDamage(SentryDamage);
        }
    }
    public IEnumerator FlameThrowing()
    {
        fireFX.Play();
        Colli.enabled = true;
        yield return new WaitForSeconds(ThrowerDur);
        fireFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        Colli.enabled = false;
        currentCD = cooldown;
    }
}
