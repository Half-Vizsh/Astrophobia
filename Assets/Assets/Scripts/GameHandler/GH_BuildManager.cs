using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.Mathematics;
using System;
using UnityEngine.Rendering;

public class GH_BuildManager : MonoBehaviour
{
    [Header("Building Stat")]
    public GameObject []TowerPrefabs = new GameObject[3];
    public bool inBuildingMode; //Modified by inventory
    public bool inDestructionMode;
    public int CurrentTWR;//For testing, on implementation, will be connected with the inventory
    [SerializeField] float BuildCD;
    [SerializeField] float BuildRad;
    public Ply_Inventory InvAccess;
    private float currentCD;
    [Header("Available Land Check")]
    Dictionary<Vector3Int, GameObject> occupiedLand = new();
    public Tilemap TMP; //Insert the empty Tower Tile layer, forgot why, but sure it's the foundation
    public Tilemap ObstacleTile;
    public Camera Cam;
    public GameObject player;
    Vector3 MouseScreenPos; Vector3 MouseWorldPos; Vector3 GridWorldPos; Vector3Int CellPos;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvAccess = player.GetComponent<Ply_Inventory>();
        inBuildingMode = false;
        inDestructionMode = false;
    }
    void Update()
    {
        CurrentTWR = InvAccess.currentChoice;
        if (currentCD>0) currentCD-=Time.deltaTime; 
        Debug.Log ("Current choice is "+CurrentTWR);
        
        if (CurrentTWR >=0&&CurrentTWR<=2) {inBuildingMode = true; inDestructionMode=false;}
        else if (CurrentTWR == 3) {inDestructionMode = true; inBuildingMode = false;}
    
        gettingPositions();
        if (inBuildingMode)
        {
            if (Mouse.current.rightButton.isPressed&&currentCD<=0)
            {
                if (canPlace(CellPos, MouseWorldPos))
                {
                    GameObject tower = Instantiate(TowerPrefabs[CurrentTWR], GridWorldPos, quaternion.identity);
                    occupiedLand.Add(CellPos, tower);
                    currentCD+=BuildCD;
                }
            }
        }
        if (inDestructionMode)
        {
            if (occupiedLand.ContainsKey(CellPos))
            {
                GameObject TowerThere = occupiedLand[CellPos];
                SpriteRenderer[] allSr = TowerThere.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer sr in allSr)
                {
                    sr.color = Color.red;
                }
                if (Mouse.current.rightButton.isPressed&&Time.time>=currentCD)
                {
                    Destroy (TowerThere);
                }    
            }    
    }
    bool canPlace(Vector3Int cell, UnityEngine.Vector3 MousePos)
        {
        Vector3Int playerCell = TMP.WorldToCell(player.transform.position);
        float dist = UnityEngine.Vector2.Distance(player.transform.position, MousePos);
        if (dist <=1.5) dist = MathF.Floor(dist); 
        return  cell != playerCell 
                && dist <= BuildRad //Can only placed sentry in 8 surrounding tiles, tweak it in the inspector
                && !occupiedLand.ContainsKey(cell)
                &&!ObstacleTile.HasTile(cell);
        }
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
        if(player == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.transform.position, BuildRad);
    }
}