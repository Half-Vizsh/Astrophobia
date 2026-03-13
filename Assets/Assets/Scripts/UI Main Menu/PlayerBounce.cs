using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
   public float speed = 5f;
    public float rotationSpeed = 180f;

    private Vector2 direction;

    void Start()
    {
        // random starting direction
        direction = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        // move
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // rotate
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        CheckScreenBounds();
    }

    void CheckScreenBounds()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPos.x <= 0 || viewPos.x >= 1)
            direction.x *= -1;

        if (viewPos.y <= 0 || viewPos.y >= 1)
            direction.y *= -1;
    }
}
