using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer instance;
    public AllAudioClips listOfClips;
    public Slider sfx;
    public Slider music;
    public Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
    private List<AudioSource> pool = new List<AudioSource>();

    public float SFXvolume=1;
    public float MusicVolume=1;
    //public                 AudioSource adiosauce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ClearCache();
        if (instance != null && instance != this) { Destroy(transform.gameObject); return; } else { instance = this; }
        
        if (clips.Count != listOfClips.listOfClips.Length)
        {
            for (int i = 0; i < listOfClips.listOfClips.Length; i++)
            {
                clips.Add(listOfClips.listOfClips[i].name, listOfClips.listOfClips[i]);
            }
        }

        DontDestroyOnLoad(transform.gameObject);
    }
    public void ClearCache()
    {
        pool = new List<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        SFXvolume = sfx.value;
        MusicVolume = music.value;
        //adiosauce.volume = SFXvolume;
    }
    public void Play(string clipName, float pitch = 1, Vector3 location = default(Vector3), float space = 0)
    {
        if(clips.TryGetValue(clipName,out AudioClip value))
        {
            for (int i = 0; i<pool.Count+1; i++) 
            {

                if (i >= pool.Count)
                {
                    GameObject AddSource = new GameObject();
                    pool.Add(AddSource.AddComponent<AudioSource>());
                }
            
              AudioSource adiosauce = pool[i];
                if (adiosauce==null) { ClearCache(); Play(clipName, pitch, location, space); ;Debug.Log("Hoorah"); return; }
                if (!adiosauce.isPlaying)
                {
                    adiosauce.transform.position = location;
                    adiosauce.clip = value;
                    adiosauce.spatialBlend = space;
                    adiosauce.pitch = pitch;
                    adiosauce.volume = SFXvolume;
                    adiosauce.Play();
                    return;
                }
            }
        }
        else { Debug.LogError("Could not find audioclip by name of: " + clipName); }

    }
    public void StopSound(string clipName)
    {
        if (clips.TryGetValue(clipName, out AudioClip value))
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].clip == value)
                {
                    pool[i].Stop();
                }
            }
        }
        else { Debug.LogError("Could not find audioclip by name of: " + clipName); }

    }
    public void Click(string clipName)
    {
        AudioSource source;
        if (GameObject.Find(clipName) != null)
        {
            source = GameObject.Find(clipName).GetComponent<AudioSource>();
        }
        else
        {
            GameObject AddSource = new GameObject(clipName);
            source = AddSource.AddComponent<AudioSource>();
            source.clip = clips[clipName];
        }

        //source.volume = Settings.settings.masterVolume;
        if (source.isPlaying == false)
        {
            source.Play();
        }

    }
    public void Song(string songName)
    {
        AudioSource source = transform.GetComponent<AudioSource>();
        if (clips.TryGetValue(songName,out AudioClip musicSwitchTo))
        {
            if (musicSwitchTo!=source.clip)
            {
                StopCoroutine("Halt");
                IEnumerator co = Halt(musicSwitchTo);
                StartCoroutine(co);
            }

        }
    }

    private IEnumerator Halt(AudioClip musicSwitchTo)
    {
        AudioSource source = transform.GetComponent<AudioSource>();

        float timer = 0;
        while (timer < 1)
        {
            timer += 2 * Mathf.Clamp(Time.unscaledDeltaTime,0,Time.maximumDeltaTime);
            source.volume = (1 - timer)*MusicVolume;
            yield return null;
        }
        source.volume = 0;

        source.clip = musicSwitchTo;
        source.Play();
        timer = 0;
        while (timer < 1)
        {
            timer += 2 * Mathf.Clamp(Time.unscaledDeltaTime, 0, Time.maximumDeltaTime);
            source.volume = timer*MusicVolume;
            yield return null;
        }
        source.volume = MusicVolume;
    }
}
