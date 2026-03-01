using System;
using Unity.Mathematics;
using UnityEngine;

public class Test_WindTurret : MonoBehaviour
{
    public Transform BulletPoint;
    public GameObject BulletPrefab;
    public float shotCD;
    private float NextShot;
    void Update()
    {
        if (NextShot < Time.time)
        {
            GameObject bullet = Instantiate (BulletPrefab, BulletPoint.position, transform.rotation);
            NextShot +=shotCD;
        }
    }
}
