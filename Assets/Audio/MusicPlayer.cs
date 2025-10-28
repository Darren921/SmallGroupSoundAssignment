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


public class MusicPlayer : MonoBehaviour
{
   private VolumeControl volumeControl;
    public static MusicPlayer instance { get; private set; }
    private EventInstance musicEventRef;
    [SerializeField] SoundDataBase soundData;

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TitleScreen");
        }
      
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
            PlaySceneMusic(soundData.ReturnEventReference(SoundData.SoundType.Music, "MainSong"));
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
            PlaySceneMusic(data[i].SoundEvtRef);
            return;
        }
    }

    private void PlaySceneMusic(EventReference musicEventReference)
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
