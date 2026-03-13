using System;
using UnityEngine;

public class Item_AddSupply : MonoBehaviour
{
    public String key;
    public int amount;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Ply_Inventory>().AddSupply(key, amount);
            Destroy(gameObject);
        }
    }
}
