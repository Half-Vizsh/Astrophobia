using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class Test_ThunTurret : MonoBehaviour
{
    public float thun_dmg;
    public GameObject ChainLightningPrefab;
    public Transform GunPoint;
    public float shootCD;
    public float TimeUntilNextShoot;
    // Update is called once per frame
    void Start()
    {
        TimeUntilNextShoot = 0f;
    }
    void Update()
    {
        if (Time.time>=TimeUntilNextShoot)
        {
            Debug.Log("ShitFired");
            Instantiate (ChainLightningPrefab, GunPoint.position, quaternion.identity);
            TimeUntilNextShoot+=shootCD;
        }
    }
}
