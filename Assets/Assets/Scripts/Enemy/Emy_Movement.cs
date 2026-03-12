using UnityEngine;

public class Emy_Movement : MonoBehaviour
{
    public float moveSpeed;
    public GameObject PlayerObject;
    public float distance;
    void Start()
    {
        PlayerObject = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        distance = Vector2.Distance(PlayerObject.transform.position, transform.position);
        Vector2 direction = PlayerObject.transform.position - transform.position;
        direction.Normalize();
        transform.position = Vector2.MoveTowards(transform.position, PlayerObject.transform.position, moveSpeed*Time.deltaTime);
    }
}