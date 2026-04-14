using UnityEngine;
using UnityEngine.UI;
public class AdjustSettings : MonoBehaviour
{
    public float SFXVolume=.6f;
    public float MusicVolume=.6f;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioSource MusicTester;
    [SerializeField] private AudioSource SFXTester;
    private void Start()
    {
        SettingsData data = SaveSystem.LoadSettings();
        if (data != null && sfxSlider!=null)
        {
            SFXVolume = data.SFXVolume;
            MusicVolume = data.MusicVolume;
            sfxSlider.value = SFXVolume;
            musicSlider.value = MusicVolume;
        }

    }

    public void AdjustSFX(float value)
    {
        SFXVolume = value;
        if (SFXTester)
        {
            SFXTester.volume = SFXVolume;
        }
    }
    public void AdjustMusic(float value)
    {
        MusicVolume = value;
        if (MusicTester)
        {
            MusicTester.volume = MusicVolume;
        }
    }
}
