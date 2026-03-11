using System;
using System.Collections;
using Mono.Cecil.Cil;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Twr_Ice_Main : MonoBehaviour
{
    [Header("Shooting")]
    public Transform BulletPoint;public Transform BulletPoint2;public Transform BulletPoint3;
    public GameObject BulletPrefab;
    public float shotCD;private float currentCD;
    public float bulletInterval;
    [Header("Animation")]
    public Animator animator; Twr_Rotation RotateScript;
    bool isActive;
    public float StartTime;
     void Start()
    {
        animator = GetComponentInChildren<Animator>();
        RotateScript = GetComponentInChildren<Twr_Rotation>();
        RotateScript.enabled =false;
        currentCD = shotCD; //InitialCD
        StartCoroutine(StartUp());
    }
    void Update()
    {
        if (!isActive) return;
        currentCD -= Time.deltaTime;
        if (currentCD<=0)
        {
           StartCoroutine(FiringBarrage());
           currentCD = shotCD;
        } 
    }
    IEnumerator StartUp()
    {
        yield return new WaitForSecondsRealtime (StartTime);
        animator.SetBool("isActive", true);
        RotateScript.enabled =true;
        isActive = true;
    }
    IEnumerator FiringBarrage()
    {
        animator.SetBool("isAttacking", true);
        Instantiate (BulletPrefab, BulletPoint.position, BulletPoint.rotation);
        yield return new WaitForSeconds(bulletInterval);
        Instantiate (BulletPrefab, BulletPoint2.position, BulletPoint2.rotation);
        yield return new WaitForSeconds(bulletInterval);
        Instantiate (BulletPrefab, BulletPoint3.position, BulletPoint3.rotation);
        animator.SetBool("isAttacking", false); 
        //If you want to change the interval, change the animation finished frame (60xInterval)
    }
}
