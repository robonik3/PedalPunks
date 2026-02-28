using UnityEngine;
using System.Collections;
public class EnterLevel : MonoBehaviour
{
    [SerializeField] private Animator titleText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

       GetComponent<AudioSource>().volume = AudioPlayer.instance.MusicVolume;
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
