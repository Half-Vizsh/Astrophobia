using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Ply_Health : MonoBehaviour
{
    [SerializeField] int maxHP;
    private int currentHP;
    public Image LifeUI;
    public Sprite [] LifeSprite = new Sprite [6];
    public int Health {get {return currentHP;}} 
    public bool isInvisble;
    [SerializeField] float invisibleDuration;
    private float DmgCD;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
        UpdateLife();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            currentHP --;
            UpdateLife();
        }
        if (isInvisble)
        {
            DmgCD -= Time.deltaTime;
            if (DmgCD <= 0)
            {
                isInvisble = false;
            }
        }
    }
    public void TakingDamage(int amount)
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
    public void UpdateLife()
    {
        if (currentHP>-1) LifeUI.sprite = LifeSprite[currentHP];
    }
}
