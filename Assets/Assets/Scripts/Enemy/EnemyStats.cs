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
    [Header("Ice Effect")]
    public ParticleSystem iceParticle;
    public SpriteRenderer sr;
    public Emy_Health EHSCript;
    void Start()
    {
        iceParticle.Stop();
    }
    public IEnumerator BeingSlowed(float duration, float slowedSpeed, float slowedAcc)
{
    float realSpeed = maxSpeed;
    float realAcc = acceleration;
    iceParticle.Play();

    if (!EHSCript.isBurning) sr.color = Color.blue;
    if (maxSpeed>slowedSpeed)maxSpeed = slowedSpeed;
    if (acceleration>slowedAcc)acceleration = slowedAcc;
    yield return new WaitForSeconds(duration);

    iceParticle.Stop();
    if (!EHSCript.isBurning)sr.color = Color.white;
    maxSpeed = realSpeed;
    acceleration = realAcc;
}
}