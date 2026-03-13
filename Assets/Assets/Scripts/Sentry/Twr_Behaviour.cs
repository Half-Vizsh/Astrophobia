using UnityEngine;
using System.Collections;

public abstract class Twr_Behaviour : MonoBehaviour
{
    [Header("Attack")]
    private float currentCD; public float shotCD; public float InitialCD; 
    public GameObject PrjPrefab; public Transform AttackPos;
    public int Ammo;
    [Header("Animation")]
    public Twr_Rotation RotateScript; public Animator animator;
    public bool isActive; public float StartTime;
    [Header("Disable Logic")]
    int DisableCount = 0;
    public bool isDisabled => DisableCount > 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        RotateScript = GetComponentInChildren<Twr_Rotation>();
        RotateScript.enabled =false;
        currentCD = InitialCD; //InitialCD
        StartCoroutine(StartUp());
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!isActive) return;
        if (isDisabled){animator.SetBool("isActive", false); RotateScript.enabled = false; return;}
        else {
            if (animator.GetBool("isActive") != !isDisabled) 
            animator.SetBool("isActive", !isDisabled);
            RotateScript.enabled = true;
            }
        currentCD -= Time.deltaTime;
        if (currentCD<=0&&Ammo>0&&RotateScript.TargetExist)
        {
           StartCoroutine(Attack());
           Ammo--;
           currentCD = shotCD;
        } 
    }
    public void EnableTower()
    {
        DisableCount = Mathf.Max(0, DisableCount-1);
    }
    public void DisableTower()
    {
        DisableCount++;
    }
    public IEnumerator StartUp()   
    {
        yield return new WaitForSeconds (StartTime);
        animator.SetBool("isActive", true);
        RotateScript.enabled =true;
        isActive = true;
    }
    public abstract IEnumerator Attack();
}
