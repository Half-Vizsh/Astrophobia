using System;
using UnityEngine;

public class Dummy_TakingDamage : MonoBehaviour
{
    public int currentHP;
    public int maxHP;
    private bool isImmune;
    public float immuneTime;
    private float currentITime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentITime>Mathf.Epsilon) currentITime -= Time.deltaTime;
        else isImmune = false;
    }
    public void TakingDamage(int amount)
    {
        if (isImmune) return;
        currentHP-=amount;
        currentITime = immuneTime;
        isImmune = true;
        if (currentHP<=Mathf.Epsilon)
        {
            Destroy(gameObject);
        }
    }
}
