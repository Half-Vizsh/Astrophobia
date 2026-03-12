using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 3f;
    public float acceleration = 6f;
    public float turnSpeed = 270f;

    [Header("Swarm")]
    public float separationRadius = 0.5f;
    public float separationStrength = 1.8f;
    
    public IEnumerator BeingSlowed(float duration, float slowedSpeed, float slowedAcc)
    {
        float RealSpeed = maxSpeed; float RealAcc = acceleration;
        Mathf.Clamp(maxSpeed, 1, slowedSpeed); Mathf.Clamp(acceleration, 1, slowedAcc);
        yield return new WaitForSeconds(duration);
        maxSpeed = RealSpeed; acceleration = RealAcc;
    }
}