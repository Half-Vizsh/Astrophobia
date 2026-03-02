using UnityEngine;

public class Test_ThundTurret : MonoBehaviour
{
    public Transform rayPoint;
    public GameObject rayPrefab;
    public float shotCD;
    private float NextShot;
     void Start()
    {
        //Initial cooldown
        NextShot = 3f;
    }
    void Update()
    {
        if (NextShot < Time.time)
        {
            GameObject ray = Instantiate (rayPrefab, rayPoint.position, transform.rotation);
            NextShot +=shotCD;
        }
    }
}
