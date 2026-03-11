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
}