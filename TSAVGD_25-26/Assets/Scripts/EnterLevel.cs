using UnityEngine;
using System.Collections;
public class EnterLevel : MonoBehaviour
{
    [SerializeField] private Animator titleText;
    [SerializeField] private bool isMusic =true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (isMusic)
        {
            GetComponent<AudioSource>().volume = AudioPlayer.instance.MusicVolume;

        }
        else
        {
            GetComponent<AudioSource>().volume = AudioPlayer.instance.SFXvolume;

        }

        if (titleText != null)
        {
            StartCoroutine(textFadeIn());
        }
    }
    IEnumerator textFadeIn()
    {
        yield return new WaitForSecondsRealtime(3);
        titleText.Play("SlideFadeOut");
    }
}
