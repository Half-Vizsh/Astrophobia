using UnityEngine;

public class Twr_Rotation : MonoBehaviour
{
    public bool IsTargetting;
    public float rotateSpeed;
    public float attackRange = 5f;
    public LayerMask enemyLayer;
    Transform NearbyEnemy;
    void Update()
    {
        NearbyEnemy = GetClosestEnemy();
        if (NearbyEnemy != null)
        {
            RotateTowards(NearbyEnemy);
        }
    }

    Transform GetClosestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            float dist = (hit.transform.position - transform.position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                closest = hit.transform;
            }
        }
        return closest;
    }
    void RotateTowards(Transform target)
    {
        Vector2 dir = target.position - transform.position;
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Color for when the object is selected
        Gizmos.DrawSphere(transform.position, attackRange); // Draws a sphere (appears as a circle in 2D view)
    }
}
