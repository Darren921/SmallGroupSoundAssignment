using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [Serializable]
    public class BusAndVolumeData
    {
        public Bus Bus;
        public float Volume = 1f;
    }

    [SerializeField]  private BusAndVolumeData MasterBus; 
    [SerializeField]  private BusAndVolumeData MusicBus; 
    [SerializeField] private BusAndVolumeData SFXBus; 
    [SerializeField] private BusAndVolumeData InterfaceBus;
    [SerializeField] private BusAndVolumeData VOBus;
    [SerializeField] private List<Slider> volumeSliders;
    private GameObject VolumeControllerUIref;

    public static VolumeControl Instance;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneManagerOnActiveSceneChanged;
       SetUpVolumeSliders();
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    
    }

    private void ValueChangeCheck(float volume , Slider slider )
    {
        if (slider.name.Contains("Master"))
            MasterBus.Volume = volume;
        else if (slider.name.Contains("Music"))
            MusicBus.Volume = volume;
        else if (slider.name.Contains("SFX"))
            SFXBus.Volume = volume;
        else if (slider.name.Contains("Interface"))
            InterfaceBus.Volume = volume;
        else if (slider.name.Contains("VO"))
            VOBus.Volume = volume;
        
        ApplyVolumeChange();
    }

    private void CheckSliderValues()
    {
        foreach (var slider in volumeSliders)
        {
            if (slider.name.Contains("Master"))
                slider.value = MasterBus.Volume;
            else if (slider.name.Contains("Music"))
                slider.value = MusicBus.Volume;
            else if (slider.name.Contains("SFX"))
                slider.value = SFXBus.Volume; 
            else if (slider.name.Contains("Interface"))
                slider.value =  InterfaceBus.Volume;
            else if (slider.name.Contains("VO"))
                slider.value =  VOBus.Volume ;
        }
    }

    private void ApplyVolumeChange()
    {
        MasterBus.Bus.setVolume(MasterBus.Volume);
        MusicBus.Bus.setVolume(MusicBus.Volume);
        SFXBus.Bus.setVolume(SFXBus.Volume);
        InterfaceBus.Bus.setVolume(InterfaceBus.Volume);
        VOBus.Bus.setVolume(VOBus.Volume);    
        
    }

    private void Start()
    {
        MasterBus.Bus = RuntimeManager.GetBus("bus:/");
        MusicBus.Bus = RuntimeManager.GetBus("bus:/Music");
        SFXBus.Bus = RuntimeManager.GetBus("bus:/SFX");
        InterfaceBus.Bus = RuntimeManager.GetBus("bus:/Interface");
        VOBus.Bus = RuntimeManager.GetBus("bus:/VO");
        ApplyVolumeChange();

    }

    private void SceneManagerOnActiveSceneChanged(Scene arg0, LoadSceneMode loadSceneMode)
    {
        volumeSliders.Clear();
        SetUpVolumeSliders();
        CheckSliderValues();
    }

    private void SetUpVolumeSliders()
    {
       var  VolumeControllerUI = GameObject.FindWithTag("VolumeControl");
        VolumeControllerUIref = VolumeControllerUI;
        SlidersSetUp(VolumeControllerUIref);
        VolumeControllerUIref.SetActive(false);

    }

    private void SlidersSetUp(GameObject VolumeControllerUI)
    {
        volumeSliders?.AddRange(VolumeControllerUIref.GetComponentsInChildren<Slider>());
        foreach (var slider in volumeSliders)
        {
            slider.onValueChanged.AddListener (val  => { ValueChangeCheck(val, slider); });
        }
    }
}
