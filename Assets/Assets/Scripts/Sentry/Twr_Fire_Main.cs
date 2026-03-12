using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class Twr_Fire_Main : Twr_Behaviour 
{
    [Header("Attack")]
    public float FlameDuration;
    public override IEnumerator Attack()
    {
        animator.SetBool("isAttacking", true);
        RotateScript.enabled = false;
        GameObject Flamethrower = Instantiate (PrjPrefab, AttackPos.position, AttackPos.rotation);
        Flamethrower.GetComponent<Prj_FireFlame>().Spreadingtime = FlameDuration;
        yield return new WaitForSeconds(FlameDuration);
        animator.SetBool("isAttacking", false);
        RotateScript.enabled = true;
        //If you want to change the interval, change the animation finished frame (60xInterval)
    }
}
