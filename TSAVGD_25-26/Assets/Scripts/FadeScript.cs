using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float fadeDuration = 0.5f;
    Image image;
    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(FadeOut());
    }
    public IEnumerator FadeOut()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime/fadeDuration;
            image.color = new Color(0, 0, 0, alpha);
            yield return null;
           
        }
    }
    public IEnumerator FadeIn()
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeDuration;
            image.color = new Color(0, 0, 0, alpha);
            yield return null;
           
        }
    }
}
