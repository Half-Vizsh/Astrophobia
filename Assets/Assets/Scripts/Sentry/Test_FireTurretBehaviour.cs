using UnityEngine;

public class Test_FireTurretBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool enemyDetected;
    public int SentryDamage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Dummy")
        {
            Dummy_TakingDamage EnemyScript = collision.gameObject.GetComponent<Dummy_TakingDamage>();
            EnemyScript.TakingDamage(SentryDamage);
        }
    }
}
