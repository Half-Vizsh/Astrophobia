using UnityEngine;

public class Emy_Health : MonoBehaviour
{
    public int MaxHP; [SerializeField]int currentHP;
    public bool isInvisible;
    public float InvDur;
    float InvTime;
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
