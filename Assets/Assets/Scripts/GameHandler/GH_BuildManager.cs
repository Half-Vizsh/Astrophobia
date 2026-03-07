using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.Mathematics;
using System;

public class GH_BuildManager : MonoBehaviour
{
    [Header("Building Stat")]
    public GameObject []TowerPrefabs = new GameObject[3];
    public bool inBuildingMode; //Modified by inventory
    public int CurrentTWR;//For testing, on implementation, will be connected with the inventory
    [SerializeField] float BuildCD;
    [SerializeField] float BuildRad;
    private float currentCD;
    [Header("Available Land Check")]
    public Tilemap TMP; //Insert the empty Tower Tile layer, forgot why, but sure it's the foundation
    public Tilemap ObstacleTile;
    public Camera Cam;
    public GameObject player;
    Dictionary<Vector3Int, GameObject> occupiedLand = new();
    void Update()
    {
        if (inBuildingMode)
        {
            UnityEngine.Vector3 MouseScreenPos = Mouse.current.position.ReadValue(); 
            UnityEngine.Vector3 MouseWorldPos =Cam.ScreenToWorldPoint(MouseScreenPos);
            MouseWorldPos.z = 0;
            Vector3Int CellPos = TMP.WorldToCell(MouseWorldPos);
            UnityEngine.Vector3 GridWorldPos = TMP.GetCellCenterWorld(CellPos);
            if (Mouse.current.rightButton.isPressed&&Time.time>=currentCD)
            {
                if (canPlace(CellPos, MouseWorldPos))
                {
                    GameObject tower = Instantiate(TowerPrefabs[CurrentTWR], GridWorldPos, quaternion.identity);
                    occupiedLand.Add(CellPos, tower);
                    currentCD+=BuildCD;
                }
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
    void OnDrawGizmos()
    {
        if(player == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.transform.position, BuildRad);
    }
}
