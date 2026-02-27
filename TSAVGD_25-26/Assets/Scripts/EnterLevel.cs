using UnityEngine;

public class EnterLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

       GameObject.Find("LevelMusic").GetComponent<AudioSource>().volume = AudioPlayer.instance.MusicVolume;

    }

}
