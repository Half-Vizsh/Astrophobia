using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class Twr_Fire_Main : MonoBehaviour
{
    [Header("Attack")]
    private float currentCD; public float shotCD; public float InitialCD; 
    public GameObject FlamePrefab; public Transform FlamePos;
    public float FlameDuration;
    [Header("Animation")]
    public Twr_Rotation RotateScript; private Animator animator;
    public bool isActive; public float StartTime;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        RotateScript = GetComponentInChildren<Twr_Rotation>();
        RotateScript.enabled =false;
        currentCD = InitialCD; //InitialCD
        StartCoroutine(StartUp());
    }
    void Update()
    {
        if (!isActive) return;
        currentCD -= Time.deltaTime;
        if (currentCD<=0)
        {
           StartCoroutine(SpreadFlame());
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
    IEnumerator SpreadFlame()
    {
        animator.SetBool("isAttacking", true);
        RotateScript.enabled = false;
        GameObject Flamethrower = Instantiate (FlamePrefab, FlamePos.position, FlamePos.rotation);
        Flamethrower.GetComponent<Prj_FireFlame>().Spreadingtime = FlameDuration;
        yield return new WaitForSeconds(FlameDuration);
        animator.SetBool("isAttacking", false);
        RotateScript.enabled = true;
        //If you want to change the interval, change the animation finished frame (60xInterval)
    }
}
