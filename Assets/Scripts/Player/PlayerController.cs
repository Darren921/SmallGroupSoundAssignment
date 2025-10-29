using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, Controls.IPlayerActions
{
    
    Controls controls;
    PlayerMovement playerMovement;
   internal Rigidbody rb;
   internal static bool inCombat;
   public static Action CombatStarted;
   public static Action CombatStopped;
   private bool CanInteract;
   private NPC currentNpc;
   internal bool inShop;
   private bool isAttacking;


   private void Awake()
    {
        CombatStarted += CombatStartedAction;
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
        controls = new Controls();
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;
        controls.Player.Interact.performed += OnInteract;
        controls.Player.ConbatOptions.performed += OnConbatOptions;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        OnEnablePlayer();
    }

    private void CombatStartedAction()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SoundManager.instance.PlayMusic(SoundManager.instance.soundData.ReturnEventReference(SoundData.SoundType.Music, "dummyTrack"));
        playerMovement.enabled = false;
        playerMovement.SetZero();
        UIControls.instance.OpenCombatUI();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            CanInteract = true;
            currentNpc = other.GetComponent<NPC>();
        }
    }

    private void OnEnablePlayer()
    {
        controls.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMovement.movement = context.ReadValue<Vector3>();
        
    }

   

    

    public void OnInteract(InputAction.CallbackContext context)
    {
        inShop = !inShop;
        if (CanInteract)
        {
            if (currentNpc != null && currentNpc.name == "ShopKeeper")
            {
                if (inShop)
                {
                    UIControls.instance.OpenShopMenu();
                }
                else
                {
                    UIControls.instance.CloseShopMenu();
                }

            }
        }
        
    }

    public void OnConbatOptions(InputAction.CallbackContext context)
    {
        var choice = context.ReadValue<float>();
        print (choice);
        switch (choice)
        {
            case 1:
                if(isAttacking) return;
                StartCoroutine(PerformAttack());
                break;
            case 2:
                Retreat();
                break;
            default:
                Debug.Log("Invalid choice" + choice);
                break;
        }
    }
    
    private IEnumerator PerformAttack()
    {
        isAttacking = true;
       SoundManager.instance.PlayOneShot(SoundManager.instance.soundData.ReturnEventReference(SoundData.SoundType.VO, "strike"),transform.position);
       yield return new WaitForSeconds(1f);
       SoundManager.instance.PlayOneShot(SoundManager.instance.soundData.ReturnEventReference(SoundData.SoundType.SFX, "Hit"),transform.position);
       isAttacking = false;

    }
    
    
    private void Retreat()
    {
        playerMovement.enabled = true;
        playerMovement.ResetPlayer();
       UIControls.instance.CloseCombatMenu();
       SoundManager.instance.PlayMusic(SoundManager.instance.soundData.ReturnEventReference(SoundData.SoundType.Music, "mainMusic"));

    }
}
