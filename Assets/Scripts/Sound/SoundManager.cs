using System;
using System.Collections.Generic;
using System.IO;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;


public class SoundManager : MonoBehaviour
{
   private VolumeControl volumeControl;
    public static SoundManager instance { get; private set; }
    private EventInstance musicEventRef;
    [SerializeField] internal SoundDataBase soundData;

    
    private void Update()
    {
        
    }

   
    
    private List<EventInstance> _eventInstances = new List<EventInstance>();
    private EventInstance currentPlaying;

    private void Start()
    {
       SceneManager.activeSceneChanged += OnSceneChanged;
        if (instance == null)
        {
            instance = this;
            volumeControl = VolumeControl.Instance;
            PlayMusic(soundData.ReturnEventReference(SoundData.SoundType.Music, "MainMusic"));
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnSceneChanged(Scene lastScene , Scene nextScene)
    {
        
        var data = soundData.Sounds;
        for (var i = 0; i < soundData.Sounds.Count; i++)
        {
            if (!data[i].IsSceneSpecific || data[i].SceneBound != nextScene.name ||
                soundData.name == data[i].SoundName) continue;
            PlayMusic(data[i].SoundEvtRef);
            return;
        }
    }

    public void PlayMusic(EventReference musicEventReference)
    {
        currentPlaying.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicEventRef = CreateEventInstance(musicEventReference) ;
        musicEventRef.start();
        musicEventRef.release();
        currentPlaying = musicEventRef;
    }

  
    public EventInstance CreateEventInstance(EventReference eventRef)
    {
        var eventInstance = RuntimeManager.CreateInstance(eventRef);
        _eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public void PlayOneShot(EventReference sound, Vector3 position )
    {
        RuntimeManager.PlayOneShot(sound, position);
    }

    

  
}
