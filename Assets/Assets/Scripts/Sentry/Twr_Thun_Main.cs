using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Twr_Thun_Main : MonoBehaviour
{
    [Header("Shooting")]
    public Transform rayPoint;
    public GameObject rayPrefab;
    public float shotCD;
    private float currentCD;
    [Header("Animation")]
    public Animator animator;
    public float animDur;
    public float StartTime;
    Twr_Rotation RotateScript;
    bool isActive;
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
           StartCoroutine(ShootRay());
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
    IEnumerator ShootRay()
    {
        RotateScript.enabled = false;
        animator.SetBool("isAttacking", true);
        GameObject ray = Instantiate (rayPrefab, rayPoint.position, transform.rotation);
        yield return new WaitForSeconds(animDur);
        animator.SetBool("isAttacking", false);
        RotateScript.enabled = true;
    }
}
