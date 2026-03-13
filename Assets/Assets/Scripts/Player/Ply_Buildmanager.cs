using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.Mathematics;
using System;
using UnityEngine.Rendering;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System.Collections;

public class Ply_Buildmanager : MonoBehaviour
{
    [Header("Building Stat")]
    public GameObject []TowerPrefabs = new GameObject[3];
    public bool inBuildingMode; public bool inDestructionMode;
    public int CurrentTWR;
    [SerializeField] float BuildCD;
    [SerializeField] float BuildRad;
    [SerializeField] float BuildTime;
    public Ply_Inventory InvAccess;
    public bool isBuildingSmt;
    private Rigidbody2D RB2D;
    private float currentCD;
    [Header("Available Land Check")]
    public Tilemap TMP; //Insert the empty Tower Tile layer, forgot why, but sure it's the foundation
    public Tilemap ObstacleTile;
    public Camera Cam;
    // public GameObject player;
    Dictionary<Vector3Int, GameObject> occupiedLand = new();
    Vector3 MouseScreenPos; Vector3 MouseWorldPos; Vector3 GridWorldPos; Vector3Int CellPos;
    void Start()
    {
        //WARNING SPAGHETTI CODE AHEAD
        InvAccess = GetComponent<Ply_Inventory>();
        inBuildingMode = false;
        inDestructionMode = false;
    }
    void Update()
    {
        CurrentTWR = InvAccess.currentChoice;
        if (currentCD>0) currentCD-=Time.deltaTime; 
        if (CurrentTWR >=0&&CurrentTWR<=2) {inBuildingMode = true; inDestructionMode=false;}
        else if (CurrentTWR == 3) {inDestructionMode = true; inBuildingMode = false;}
    
        gettingPositions();
        if (inBuildingMode)
        {
            if (canPlace(CellPos, MouseWorldPos))
            {
                //Try instantiating a cursor of some sort here
                if (Mouse.current.rightButton.wasPressedThisFrame&&currentCD<=0)
                {
                    StartCoroutine(BuildSomething());
                }
            }
        }
        if (inDestructionMode)
        {
            if (occupiedLand.ContainsKey(CellPos))
            {
                GameObject TowerThere = occupiedLand[CellPos];
                if (TowerThere == null) return;
                SpriteRenderer[] allSr = TowerThere.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer sr in allSr)
                {
                    sr.color = Color.red;
                }
                if (Mouse.current.rightButton.wasPressedThisFrame&&Time.time>=currentCD)
                {
                    Destroy (TowerThere);
                }    
            }    
        }
    }
    IEnumerator BuildSomething()
    {
        String towerKey;
        switch (CurrentTWR){ 
            case 0:towerKey = "Thun"; break;
            case 1:towerKey = "Fire";break;
            case 2:towerKey = "Ice";break;
            default: towerKey = "Not Exist"; break; 
        }
        if (towerKey.Equals("Not Exist")) yield break;
        if (InvAccess.PlayerInventory.ContainsKey(towerKey)&&InvAccess.PlayerInventory[towerKey]>0)
        {
            isBuildingSmt = true;
            GameObject tower = Instantiate(TowerPrefabs[CurrentTWR], GridWorldPos, quaternion.identity);
            InvAccess.PlayerInventory[towerKey]--;
            InvAccess.UpdateInventory(towerKey);
            if (InvAccess.PlayerInventory[towerKey]<=0) StartCoroutine(InvAccess.EmergencyRefill(towerKey));
            Debug.Log (towerKey + "Has been spawned, you have "+InvAccess.PlayerInventory[towerKey]+" left");
            occupiedLand.Add(CellPos, tower);
            currentCD+=BuildCD;
            
            yield return new WaitForSeconds (BuildTime);
            isBuildingSmt = false;
        }
    }
    //Checking whether the grid is empty or not
    bool canPlace(Vector3Int cell, UnityEngine.Vector3 MousePos)
        {
        Vector3Int playerCell = TMP.WorldToCell(transform.position);
        float dist = UnityEngine.Vector2.Distance(transform.position, MousePos);
        if (dist <=1.5) dist = MathF.Floor(dist); 
        return  cell != playerCell 
                && dist <= BuildRad //Can only placed sentry in 8 surrounding tiles, tweak it in the inspector
                && !occupiedLand.ContainsKey(cell)
                &&!ObstacleTile.HasTile(cell);
        }
    void gettingPositions()
    {
        MouseScreenPos = Mouse.current.position.ReadValue(); 
        MouseWorldPos =Cam.ScreenToWorldPoint(MouseScreenPos);
        MouseWorldPos.z = 0;
        CellPos = TMP.WorldToCell(MouseWorldPos);
        GridWorldPos = TMP.GetCellCenterWorld(CellPos);
    }
    void OnDrawGizmos()
    {
        // if(player == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, BuildRad);
    }
}