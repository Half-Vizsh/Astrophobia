using System;
using System.Collections;
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
    public bool isInvisble; public bool spriteWhite;
    [SerializeField] float invisibleDuration; 
    private float DmgCD;
    public Ply_Movement moveScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveScript = GetComponent<Ply_Movement>();
        currentHP = maxHP;
        UpdateLife();
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
    public void TakingDamage(int amount, Vector2 enemyDir)
    {
        if (isInvisble) return;
        DmgCD = invisibleDuration;
        isInvisble = true;
        currentHP = Math.Clamp(currentHP-amount, 0, maxHP);
        StartCoroutine(WhenTakeDamage());
        if (enemyDir!=Vector2.zero)StartCoroutine(moveScript.Knockback(enemyDir));
        UpdateLife();
    }
    public void UpdateLife()
    {
        if (currentHP>-1) LifeUI.sprite = LifeSprite[currentHP];
    }
    public IEnumerator WhenTakeDamage()
    {
        //Modify sprite?
        isInvisble = true;
        spriteWhite = true;
        yield return new WaitForSeconds (0.1f);
        spriteWhite = false;
        yield return new WaitForSeconds (invisibleDuration);
        isInvisble = false;
    } 
}
