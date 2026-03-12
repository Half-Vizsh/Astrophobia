using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Ply_Inventory : MonoBehaviour
{
    [Header("UI Handler")]
    public Sprite [] ImgSelected = new Sprite [4];
    public Sprite [] notSelected = new Sprite [4];
    public Image [] hotbarElements = new Image [4];
    [Header("Input Handler")]
    public Dictionary<String, int> PlayerInventory = new (); 
    // public int cound_Ice; public int count_Fire; public int count_Thund; //public int count_Destr;
    public Ply_Buildmanager BuildManager;
    public PlayerInputActions HotbarInput;
    public int currentChoice; // This will be given to the BuildManager
    [SerializeField] private int initialSupply;
    void Awake()
    {
        HotbarInput = new PlayerInputActions();
        BuildManager = GetComponent<Ply_Buildmanager>();
        PlayerInventory.Add("Thun", initialSupply);
        PlayerInventory.Add("Fire", initialSupply);
        PlayerInventory.Add("Ice", initialSupply);
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
