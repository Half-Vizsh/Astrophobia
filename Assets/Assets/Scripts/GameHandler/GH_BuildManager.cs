using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.Mathematics;

public class GH_BuildManager : MonoBehaviour
{
    [Header("Building Stat")]
    public GameObject []TowerPrefabs = new GameObject[3];
    public bool inBuildingMode; //Yet been modified
    public int CurrentTWR;//For testing, on implementation, will be connected with the inventory
    [SerializeField] float BuildCD;
    private float currentCD;
    [Header("Available Land Check")]
    public Tilemap TMP; //Insert the Tower Tile layer, I forgot why, just do it
    public Tilemap ObstacleTile;
    public Camera Cam;
    Dictionary<Vector3Int, GameObject> occupiedLand = new();
    void Update()
    {
        if (inBuildingMode)
        {
            UnityEngine.Vector3 MouseScreenPos = Mouse.current.position.ReadValue(); 
            UnityEngine.Vector3 MouseWorldPos =Cam.ScreenToWorldPoint(MouseScreenPos);
            //Debug.Log("The MouseWorldPos is "+MouseWorldPos);
            MouseWorldPos.z = 0;
            Vector3Int CellPos = TMP.WorldToCell(MouseWorldPos);
            UnityEngine.Vector3 GridWorldPos = TMP.GetCellCenterWorld(CellPos);
            //Debug.Log("The GridtoWorldPos is "+GridWorldPos);
            if (Mouse.current.rightButton.isPressed&&Time.time>=currentCD)
            {
                if (canPlace(CellPos))
                {
                    GameObject tower = Instantiate(TowerPrefabs[CurrentTWR], GridWorldPos, quaternion.identity);
                    occupiedLand.Add(CellPos, tower);
                    currentCD+=BuildCD;
                }
            }
        }
    }
    bool canPlace(Vector3Int cell)
    {
        return !occupiedLand.ContainsKey(cell)&&!ObstacleTile.HasTile(cell);
    }
}
