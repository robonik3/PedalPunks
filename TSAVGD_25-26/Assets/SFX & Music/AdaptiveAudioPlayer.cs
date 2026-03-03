using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AdaptiveAudioPlayer : MonoBehaviour
{
    [SerializeField]private float fadeSpeed;
    public List<AudioSource> audioTracks = new List<AudioSource>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < audioTracks.Count; i++)
        {
            if (audioTracks[i].volume == 1)
            {
                audioTracks[i].volume = AudioPlayer.instance.MusicVolume;
            }
        }
    }

    public void PlayAdaptiveTrack(int track)
    {
        IEnumerator co = FadeInTrack(track);
        StartCoroutine(co);
    }
    public void StopAdaptiveTrack(int track)
    {
        IEnumerator co = FadeOutTrack(track);
        StartCoroutine(co);
    }
    IEnumerator FadeInTrack(int track)
    {
        float timer = 0;

        while (timer < 1)
        {
            timer+= Time.deltaTime * fadeSpeed;

            audioTracks[track].volume = timer * AudioPlayer.instance.MusicVolume;

            yield return null;
        }

        audioTracks[track].volume = AudioPlayer.instance.MusicVolume;

    }
    IEnumerator FadeOutTrack(int track)
    {
        if (audioTracks[track].volume != 0)
        {
            float timer = 1;

            while (timer > 0)
            {
                timer -= Time.deltaTime * fadeSpeed;

                audioTracks[track].volume = timer * AudioPlayer.instance.MusicVolume;

                yield return null;
            }
        }


        audioTracks[track].volume = 0;

    }
}
