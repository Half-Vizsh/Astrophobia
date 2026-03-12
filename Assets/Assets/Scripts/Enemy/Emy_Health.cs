using UnityEngine;

public class Emy_Health : MonoBehaviour
{
    public int MaxHP; [SerializeField]int currentHP;
    public bool isInvisible;
    public float InvDur;
    float InvTime;
    [Header("Item Drop")]
    public GameObject [] Rewardpool = new GameObject [3];
    public float dropRate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = MaxHP;   
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP <= 0)
        {
            float randomValue = Random.Range(0f,1f);
            if (randomValue <= dropRate)
            {
                Debug.Log("you're lucky");
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
}
