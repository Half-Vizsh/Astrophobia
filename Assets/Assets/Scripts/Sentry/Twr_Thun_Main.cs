using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Twr_Thun_Main : Twr_Behaviour
{
    [Header("Shooting")]
    public Transform rayPoint;
    public GameObject rayPrefab;
    public float animDur;
    public override IEnumerator Attack()
    {
        RotateScript.enabled = false;
        animator.SetBool("isAttacking", true);
        GameObject ray = Instantiate (PrjPrefab, rayPoint.position, transform.rotation);
        yield return new WaitForSeconds(animDur);
        animator.SetBool("isAttacking", false);
        RotateScript.enabled = true;
         Ammo--;
        if (Ammo<=0) {
            animator.SetBool("isActive", false);
            RotateScript.enabled = false;
            isActive = false;
        }
    }
}
