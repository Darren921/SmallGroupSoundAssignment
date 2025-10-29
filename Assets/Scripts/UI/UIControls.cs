using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIControls : MonoBehaviour, Controls.IUIActions
{
   public static UIControls instance;
    Controls controls;
    bool onScreen ;
    private GameObject ShopUI, CombatUI, volumeUIRef;
    private void Start()
    {
       if (instance == null)
       {
          instance = this;
          DontDestroyOnLoad(this);
          SceneManager.sceneLoaded += SceneManagerOnactiveSceneChanged;
          volumeUIRef = VolumeControl.Instance.VolumeControllerUIref;
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
       volumeUIRef = GameObject.FindWithTag("VolumeControl");
       ShopUI = GameObject.Find("ShopUI");
       CombatUI = GameObject.Find("CombatUI");
       
    }

    public void OpenUI(GameObject uiObject)
    {
       
    }
    public void OpenShopMenu()
    {
       SoundManager.instance.PlayOneShot(SoundManager.instance.soundData.ReturnEventReference(SoundData.SoundType.VO, "ShopKeep"), transform.position);
       Cursor.lockState = CursorLockMode.Confined;
       Time.timeScale = 0;
       ShopUI.transform.position = VolumeControl.Instance.VolumeControllerTransformOn.position;
    }
    public void CloseShopMenu()
    {
       Cursor.lockState = CursorLockMode.Locked;
       Time.timeScale = 1;
       ShopUI.transform.position = VolumeControl.Instance.VolumeControllerTransformOff.position;
    }


    private void OnEnableUI()
    {
       controls.UI.Enable();
    }

    private void OnDisableUI()
    {
       controls.UI.Disable();
    }

    public void OnPauseMenu(InputAction.CallbackContext context)
    {
       volumeUIRef.SetActive(true);
       if (!onScreen)
       {
          OpenPauseMenu();
       }
       else
       {
          ClosePauseMenu();
       }

    }
    public void OpenCombatUI()
    {
       SoundManager.instance.PlayOneShot(SoundManager.instance.soundData.ReturnEventReference(SoundData.SoundType.SFX, "bladedrawn"), transform.position);
       Cursor.lockState = CursorLockMode.Confined;
       CombatUI.transform.position = VolumeControl.Instance.VolumeControllerTransformOn.position;
    }
    public void CloseCombatMenu()
    {
       Cursor.lockState = CursorLockMode.Locked;
       CombatUI.transform.position = VolumeControl.Instance.VolumeControllerTransformOff.position;
    }
    public void OpenPauseMenu()
    {
       Cursor.lockState = CursorLockMode.Confined;
       SoundManager.instance.PlayOneShot(SoundManager.instance.soundData.ReturnEventReference(SoundData.SoundType.Interface, "MenuOpen"), transform.position);
       Time.timeScale = 0;
       volumeUIRef.transform.position = VolumeControl.Instance.VolumeControllerTransformOn.position;
       onScreen = true;
    }

    public void ClosePauseMenu()
    {
       if(SceneManager.GetActiveScene().name == "Main" && !PlayerController.inCombat) Cursor.lockState = CursorLockMode.Locked;
       volumeUIRef.transform.position = VolumeControl.Instance.VolumeControllerTransformOff.position;
       Time.timeScale = 1;
       onScreen = false;
       StartCoroutine(HideVolumeUI()) ;
    }

    private IEnumerator HideVolumeUI()
    {
       yield return new WaitForSeconds(0.1f);
       volumeUIRef.SetActive(false);
    }


   
}
