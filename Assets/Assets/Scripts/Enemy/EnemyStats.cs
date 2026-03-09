using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 3f;
<<<<<<< HEAD
    public float acceleration = 6f;
=======
    public float acceleration = 10f;
>>>>>>> b854c84a6d7aa5f8880e0befaa05c876c9192107
    public float turnSpeed = 360f;

    [Header("Swarm")]
    public float separationRadius = 0.5f;
    public float separationStrength = 1.8f;
}