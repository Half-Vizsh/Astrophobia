using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

public class Ply_Inventory : MonoBehaviour
{
    [Header("UI Handler")]
    public Sprite [] ImgSelected = new Sprite [4];
    public Sprite [] notSelected = new Sprite [4];
    public Image [] hotbarElements = new Image [4];
    public TextMeshProUGUI [] TowerStoredAmount = new TextMeshProUGUI [3];
    [Header("Input Handler")]
    public Dictionary<String, int> PlayerInventory = new (); 
    // public int cound_Ice; public int count_Fire; public int count_Thund; //public int count_Destr;
    public Ply_Buildmanager BuildManager;
    public PlayerInputActions HotbarInput;
    public int currentChoice; // This will be given to the BuildManager
    [SerializeField] private int initialSupply;
    public float RefillDur;
    void Awake()
    {
        HotbarInput = new PlayerInputActions();
        BuildManager = GetComponent<Ply_Buildmanager>();
        PlayerInventory.Add("Thun", initialSupply); UpdateInventory("Thun"); 
        PlayerInventory.Add("Fire", initialSupply); UpdateInventory("Fire");
        PlayerInventory.Add("Ice", initialSupply); UpdateInventory("Ice");
    }
    public void SelectSlot(int numPressed)
    {
        currentChoice = numPressed;
        for (int i = 0; i<hotbarElements.Length; i++)
        {
            //Changing UI image
            if (i==currentChoice) hotbarElements[i].sprite = ImgSelected [i];
            else hotbarElements[i].sprite = notSelected [i];
        }
    }
    public void AddSupply(String key, int amount)
    {
        if (PlayerInventory.ContainsKey(key)&&PlayerInventory[key]<5) 
        {
            PlayerInventory[key]+=amount;
            UpdateInventory(key);
            Debug.Log ("Add "+key+" "+amount+" to your inventory");
        } else Debug.Log("Inventory is full");
    }
    public void UpdateInventory(String TwrKey)
    {
        int index=0;
        switch(TwrKey){ case "Thun": index = 0; break; case "Fire": index = 1; break; case "Ice": index = 2; break;}
        TowerStoredAmount[index].text = PlayerInventory[TwrKey]+"/5";
    }
    public IEnumerator EmergencyRefill(String key)
    {
        yield return new WaitForSeconds(RefillDur);
        Debug.Log("Refill Active");
        // PlayerInventory[key]
        AddSupply(key, 1);
    }
    //Input for reading number
    public void OnEnable()
    {
        HotbarInput.Enable();   
        HotbarInput.Player.Slot1.performed += ctx => SelectSlot(0);
        HotbarInput.Player.Slot2.performed += ctx => SelectSlot(1);
        HotbarInput.Player.Slot3.performed += ctx => SelectSlot(2);
        HotbarInput.Player.Slot4.performed += ctx => SelectSlot(3);
    }
    public void OnDisable()
    {
        HotbarInput.Disable();
    }
}
