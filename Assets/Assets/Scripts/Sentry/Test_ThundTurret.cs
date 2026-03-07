using UnityEngine;
using UnityEngine.PlayerLoop;

public class Test_ThundTurret : MonoBehaviour
{
    public Transform rayPoint;
    public GameObject rayPrefab;
    public float shotCD;
    private float currentCD;
     void Start()
    {
        currentCD = shotCD; //InitialCD
    }
    void Update()
    {
        currentCD -= Time.deltaTime;
        if (currentCD<=0)
        {
            GameObject ray = Instantiate (rayPrefab, rayPoint.position, transform.rotation);
            currentCD = shotCD;
        }
        
    }
}
