using UnityEngine;

public class Emy_Health : MonoBehaviour
{
    public int MaxHP; [SerializeField]int currentHP;
    public bool isInvisible;
    public float InvDur;
    float InvTime;
    [Header("Item Drop")]
    public GameObject [] Rewardpool = new GameObject [3];
    public SpriteRenderer sr;
    public float dropRate;
    [Header("Burning")]
    public bool isBurning;
    float burnTimer; float burnTickTimer;int burnDamage;
    public ParticleSystem burnParticle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = MaxHP;
        sr = GetComponentInChildren<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (isBurning)
        {
            Debug.Log("HOT HOT");
            burnTimer -= Time.deltaTime;
            burnTickTimer -= Time.deltaTime;
            sr.color = Color.red;
            if (burnTickTimer <= 0f)
            {
                burnTickTimer = 1f;
                TakingDamage(burnDamage);
            }
            if (burnTimer <= 0f)
                isBurning = false;
        } else {
            burnParticle.Stop();
            sr.color = Color.white;
        }

        if (currentHP <= 0)
        {
            float randomValue = Random.Range(0f,1f);
            if (randomValue <= dropRate)
            {
                int item = Random.Range(0,3);
                Instantiate (Rewardpool[item], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        InvTime -= Time.deltaTime;
        if (InvTime <= 0) isInvisible = false;
        else isInvisible = true;
    }
    public void TakingDamage(int amount)
    {
        if (isInvisible) return;
        currentHP -= amount;
        InvTime = InvDur;
    }
    public void ApplyBurn(float duration, int damage)
    {
    isBurning = true;
    burnTimer = duration;
    burnDamage = damage;
    burnParticle.Play(); 
    }
}
