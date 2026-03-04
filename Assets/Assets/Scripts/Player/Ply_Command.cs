using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ply_Command : MonoBehaviour
{
    [SerializeField] private Camera mCamera;
    public LayerMask enemy;
    Vector2 position;
    GameObject currentEmy;
    GameObject nextEmy;
    void Start()
    {
        mCamera = GameObject.FindAnyObjectByType<Camera>();
    }
    void Update()
    {
        Vector3 MousePos = Mouse.current.position.ReadValue();
        if (Mouse.current.leftButton.isPressed) {
            position = mCamera.ScreenToWorldPoint(MousePos);
            Detection(position);
        }
    }
    public void Detection(Vector2 pos)
    {
        Collider2D ObjDetected = Physics2D.OverlapPoint(pos, enemy);
        if (ObjDetected != null)
        {
            nextEmy = ObjDetected.gameObject;
            if (currentEmy == null) currentEmy = nextEmy; //If there's no enemy previously
            else{
                currentEmy.GetComponent<Emy_BeingTarget>().isTargetted = false;
                currentEmy = nextEmy; //If new enemy were being targeted, make the old isTargetted false and switch target
            }
            currentEmy.GetComponent<Emy_BeingTarget>().isTargetted = true;
        }
    }
}
