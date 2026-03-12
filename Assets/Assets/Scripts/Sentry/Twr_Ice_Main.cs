using System;
using System.Collections;
using Mono.Cecil.Cil;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Twr_Ice_Main : Twr_Behaviour
{
    [Header("Shooting")]
    public Transform BulletPoint;public Transform BulletPoint2;public Transform BulletPoint3;
    public float bulletInterval; //Ice doesn't need to modify attackPos as it have it's own
    public override IEnumerator Attack()
    {
        animator.SetBool("isAttacking", true);
        Instantiate (PrjPrefab, BulletPoint.position, BulletPoint.rotation);
        yield return new WaitForSeconds(bulletInterval);
        Instantiate (PrjPrefab, BulletPoint2.position, BulletPoint2.rotation);
        yield return new WaitForSeconds(bulletInterval);
        Instantiate (PrjPrefab, BulletPoint3.position, BulletPoint3.rotation);
        animator.SetBool("isAttacking", false); 
        Ammo--;
        if (Ammo<=0) {
            animator.SetBool("isActive", false);
            RotateScript.enabled = false;
            isActive = false;
           }
        //If you want to change the interval, change the animation finished frame (60xInterval)
    }
}
