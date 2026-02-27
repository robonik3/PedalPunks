using System;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class SettingsData
{
    public float SFXVolume;
    public float MusicVolume;
    public SettingsData(AdjustSettings adjustSettings)
    {
        SFXVolume = adjustSettings.SFXVolume;
        MusicVolume = adjustSettings.MusicVolume;

    }
}
