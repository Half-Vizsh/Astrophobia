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
        currentCD -= Time.deltaTime;
        if (currentCD<=0&&Ammo>0)
        {
           StartCoroutine(Attack());
           Ammo--;
           currentCD = shotCD;
        } 
    }
    public IEnumerator StartUp()   
    {
        yield return new WaitForSecondsRealtime (StartTime);
        animator.SetBool("isActive", true);
        RotateScript.enabled =true;
        isActive = true;
    }
    public abstract IEnumerator Attack();
}
