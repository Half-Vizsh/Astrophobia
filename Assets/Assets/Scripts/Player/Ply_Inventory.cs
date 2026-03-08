using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ply_Inventory : MonoBehaviour
{
    public Dictionary<String, int> PlayerInventory = new (); 
    // public int cound_Ice; public int count_Fire; public int count_Thund; //public int count_Destr;
    public GH_BuildManager BuildManager;
    public PlayerInputActions HotbarInput;
    public int currentChoice; // This will be given to the BuildManager
    void Awake()
    {
        HotbarInput = new PlayerInputActions();
        PlayerInventory.Add("Thun", 1);
        PlayerInventory.Add("Fire", 1);
        PlayerInventory.Add("Ice", 1);
    }
    public void SelectSlot(int numPressed)
    {
        currentChoice = numPressed;
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
