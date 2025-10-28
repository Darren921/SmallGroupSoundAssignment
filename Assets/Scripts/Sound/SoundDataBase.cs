using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SoundDataBase", menuName = "Scriptable Objects/SoundDataBase")]
public class SoundDataBase : ScriptableObject
{
    public List<SoundData> Sounds = new List<SoundData>();
    public List<string> SceneList = new List<string>();
    private void OnValidate()
    {
        GetSceneList();
        if (Sounds.Count <= 0) return;
        foreach (var sound in Sounds)
        {
            var splits = sound.SoundEvtRef.Path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (splits.Length == 3)
            {
                var soundTypeString = splits[1];
                Enum.TryParse(soundTypeString, out SoundData.SoundType SoundType);
                sound.soundType = SoundType;
            }
            sound.SoundName = sound.SoundEvtRef.Path.Split('/').Last().Replace(" ", "").ToLower();
        }
    }

    public EventReference ReturnEventReference(SoundData.SoundType soundType, string soundName)
    {
        var eventRef = new EventReference();
        eventRef = Sounds.Find(data => data.soundType == soundType && string.Equals(data.SoundName, soundName, StringComparison.CurrentCultureIgnoreCase)).SoundEvtRef;
        Debug.Log(eventRef.Path);
        return  eventRef;
    }

    private void GetSceneList()
    {
        SceneList.Clear();
        SceneList.Insert(0,"None");
        for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var sceneIndex = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            if (!SceneList.Contains(sceneIndex))
            {
                SceneList.Add(sceneIndex);
            }
        }
    }


}

[Serializable]
public class SoundData
{
    public enum  SoundType
    {
        None,
        Music, 
        SFX,
        Interface
    }
    public EventReference SoundEvtRef;
    public SoundType soundType;
    public string SoundName;
    public bool IsSceneSpecific;
    public string SceneBound;
    public SoundData( SoundType type, EventReference soundEvtRef ,string soundName, bool isSceneSpecific,string sceneBound )
    {
        soundType = type;
        SoundName = soundName;
        SoundEvtRef =  soundEvtRef;
        IsSceneSpecific = isSceneSpecific;
        SceneBound = sceneBound;
    }
}
