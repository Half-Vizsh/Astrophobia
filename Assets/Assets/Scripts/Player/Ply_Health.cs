using System;
using NUnit.Framework;
using UnityEngine;

public class Ply_Health : MonoBehaviour
{
    [SerializeField] float maxHP;
    private float currentHP;
    public float Health {get {return currentHP;}} 
    public bool isInvisble{get; private set;}
    public void setInvisble(bool State)
    {
        //"Good habit" they said
        isInvisble = State;
    }
    [SerializeField] float invisibleDuration;
    private float DmgCD;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHP = currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvisble)
        {
            DmgCD -= Time.deltaTime;
            if (DmgCD <= 0)
            {
                isInvisble = false;
            }
        }
    }
    public void TakingDamage(float amount)
    {
        if (isInvisble)
        {
            return;
        }
        DmgCD = invisibleDuration;
        isInvisble = true;
        currentHP = Math.Clamp(currentHP-amount, 0, maxHP);
        if (currentHP <= 0)
        {
            Destroy (gameObject); //Temporary, Maybe it's better to move the win/lose condition to a game manager (like in the workshop example)
        }
    }
}
