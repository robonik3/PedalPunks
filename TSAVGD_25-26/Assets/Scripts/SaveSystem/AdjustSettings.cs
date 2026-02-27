using UnityEngine;
using UnityEngine.UI;
public class AdjustSettings : MonoBehaviour
{
    public float SFXVolume=1;
    public float MusicVolume=1;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
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
    }
    public void AdjustMusic(float value)
    {
        MusicVolume = value;
    }
}
