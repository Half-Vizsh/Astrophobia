using UnityEngine;

public class Player_Health : MonoBehaviour
{
    [SerializeField] float maxHP;
    public float currentHP; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHP = currentHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP<=0)
        {
            
        }
    }
}
