using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIControls : MonoBehaviour, Controls.IUIActions
{
    static UIControls instance;
    private GameObject volumeUIRef;
    Controls controls;
    private void Start()
    {
       if (instance == null)
       {
          instance = this;
          DontDestroyOnLoad(this);
          SceneManager.sceneLoaded += SceneManagerOnactiveSceneChanged;
       }
       else
       {
          Destroy(gameObject);
       }
       controls = new Controls();
       controls.UI.PauseMenu.performed += OnPauseMenu;
       OnEnableUI();
    }

    private void SceneManagerOnactiveSceneChanged(Scene arg0, LoadSceneMode loadSceneMode)
    {
      var  VolumeUItemp = GameObject.FindWithTag("VolumeControl");
      volumeUIRef = VolumeUItemp;
    }


    private void OnEnableUI()
    {
       controls.UI.Enable();
    }

    private void OnDisableUI()
    {
       controls.UI.Disable();
    }

    public void OnNavigate(InputAction.CallbackContext context)
   {
      throw new System.NotImplementedException();
   }

   public void OnSubmit(InputAction.CallbackContext context)
   {
      throw new System.NotImplementedException();
   }

   public void OnCancel(InputAction.CallbackContext context)
   {
      throw new System.NotImplementedException();
   }

   public void OnPoint(InputAction.CallbackContext context)
   {
      throw new System.NotImplementedException();
   }

   public void OnClick(InputAction.CallbackContext context)
   {
      throw new System.NotImplementedException();
   }

   public void OnRightClick(InputAction.CallbackContext context)
   {
      throw new System.NotImplementedException();
   }

   public void OnMiddleClick(InputAction.CallbackContext context)
   {
      throw new System.NotImplementedException();
   }

   public void OnScrollWheel(InputAction.CallbackContext context)
   {
      throw new System.NotImplementedException();
   }

   public void OnTrackedDevicePosition(InputAction.CallbackContext context)
   {
      throw new System.NotImplementedException();
   }

   public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
   {
      throw new System.NotImplementedException();
   }

   public void OnPauseMenu(InputAction.CallbackContext context)
   {
      volumeUIRef.SetActive(!volumeUIRef.activeInHierarchy);
   }
}
