using System;
using System.Collections;
using Mono.Cecil.Cil;
using Unity.Mathematics;
using UnityEngine;

public class Test_WindTurret : MonoBehaviour
{
    public Transform BulletPoint;
    public Transform BulletPoint2;
    public Transform BulletPoint3;
    public GameObject BulletPrefab;
    public float shotCD;
    public float bulletInterval;
    private float NextShot;
    private int ShootCount;
    void Start()
    {
        //Initial CD
         NextShot = 3f;
    }
    void Update()
    {
        if (NextShot < Time.time)
        {
            StartCoroutine(FiringBarrage());
            NextShot+=shotCD;
        }
    }
    IEnumerator FiringBarrage()
    {
        Instantiate (BulletPrefab, BulletPoint.position, BulletPoint.rotation);
        yield return new WaitForSeconds(bulletInterval);
        Instantiate (BulletPrefab, BulletPoint2.position, BulletPoint2.rotation);
        yield return new WaitForSeconds(bulletInterval);
        Instantiate (BulletPrefab, BulletPoint3.position, BulletPoint3.rotation);
    }
}
