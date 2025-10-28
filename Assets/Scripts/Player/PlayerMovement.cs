using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   internal Vector3 movement;
   private Vector3 MoveDir;
   private float MoveSpeed = 5;
   internal PlayerController player;
   private Vector3 SmoothedMoveDir;
   private Vector3 SmoothedMoveVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        SetMoveDir(movement);
        SmoothMovement();
        ApplyVelocity();
    }

    private void ApplyVelocity()
    {
        player.rb.linearVelocity = new Vector2(SmoothedMoveDir.x * MoveSpeed,0) ;
    }

    private void SmoothMovement()
    {
        SmoothedMoveDir = Vector3.SmoothDamp(SmoothedMoveDir, MoveDir, ref SmoothedMoveVelocity, 0.1f);
    }

    private void  SetMoveDir(Vector3 newDir)
    {
        MoveDir = newDir.normalized;
    }
}
