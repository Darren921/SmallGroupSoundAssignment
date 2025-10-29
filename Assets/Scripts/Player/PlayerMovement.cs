using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
   internal Vector3 movement;
   private Vector3 MoveDir;
   private float MoveSpeed = 10;
   internal PlayerController player;
   private Vector3 SmoothedMoveDir;
   private Vector3 SmoothedMoveVelocity;
   private Camera Camera;
   private CinemachineCamera VirtualCamera;
   internal CinemachineInputAxisController CinemachineInputAxisController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GetComponent<PlayerController>();
        Camera = Camera.main;
        VirtualCamera = FindFirstObjectByType<CinemachineCamera>();
        CinemachineInputAxisController = VirtualCamera.GetComponent<CinemachineInputAxisController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        SetMoveDir(movement);
        ApplyVelocity();
        RotatePlayerTowardsCamera();
    }
    private void RotatePlayerTowardsCamera()
    {
        if (!Camera || !player.rb) return;
        var cameraForward = Camera.transform.forward;
        cameraForward.y = 0f;
        if (cameraForward == Vector3.zero) return;
        var newRotation = Quaternion.LookRotation(cameraForward);
//        Debug.Log(newRotation);
        player.rb.MoveRotation(newRotation);
        
    }
    private void ApplyVelocity()
    {
        player.rb.linearVelocity = MoveDir.normalized * MoveSpeed;
    }

    public void SetZero()
    {
        player.rb.constraints = RigidbodyConstraints.FreezeAll;
        CinemachineInputAxisController.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ResetPlayer()
    {
        player.rb.constraints = RigidbodyConstraints.None;
        CinemachineInputAxisController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void  SetMoveDir(Vector3 newDir)
    {
        var cameraForward = Camera.transform.forward;
        var cameraRight = Camera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        MoveDir = cameraForward.normalized * movement.z + cameraRight.normalized * movement.x;
    }
}
