using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, Controls.IPlayerActions
{
    
    Controls controls;
    PlayerMovement playerMovement;
   internal Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        controls = new Controls();
        controls.Player.Move.performed += OnMove;
        OnEnablePlayer();
    }

    private void OnEnablePlayer()
    {
        controls.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMovement.movement = context.ReadValue<Vector3>();
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    

    public void OnSprint(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
