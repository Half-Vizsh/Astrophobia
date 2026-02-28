using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ChainLigtningScript : MonoBehaviour
{
    private CircleCollider2D Colli;
    public LayerMask EmyLayer;
    public float Dmg;
    public GameObject ChainLigtningEffect;
    public GameObject beenStruct;
    public int amountToChain;
    private GameObject StartObject;
    public GameObject EndObject;
    private Animator ani;
    public ParticleSystem PS;
    private int singleSpawn;

    void Start()
    {
        if (amountToChain == 0) Destroy (gameObject);
        Colli = GetComponent<CircleCollider2D>();
        ani = GetComponent<Animator>();
        PS = GetComponent<ParticleSystem>();
        StartObject = gameObject;
        singleSpawn = 1;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (EmyLayer == (EmyLayer|(1 << collision.gameObject.layer))&& !collision.GetComponentInChildren<EnemyStruct>()){
            if (singleSpawn != 0){
                EndObject = collision.gameObject;

                amountToChain -=1;

                Instantiate (ChainLigtningEffect, collision.gameObject.transform.position, quaternion.identity);

                Instantiate (beenStruct, collision.gameObject.transform);

                collision.gameObject.GetComponent<Dummy_TakingDamage>();

                ani.StopPlayback();

                Colli.enabled = false;
                singleSpawn --;

                PS.Play();
                var emitParams = new ParticleSystem.EmitParams();
                emitParams.position = StartObject.transform.position;
                PS.Emit (emitParams, 1);
                emitParams.position = EndObject.transform.position;
                PS.Emit (emitParams, 1);
                
                Destroy(gameObject, 1f);
            }
        }
    }
}
