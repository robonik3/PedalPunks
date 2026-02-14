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
        float alpha = 0f;
        RectTransform rt = GetComponent<RectTransform>();
        while (alpha < 1f)
        {
            alpha += Time.deltaTime/fadeDuration;
            //image.color = new Color(0, 0, 0, alpha);
            rt.offsetMax = new Vector2(-alpha*1920,0);

            yield return null;
           
        }
        rt.offsetMax = new Vector2(-1920, 0);

    }
    public IEnumerator FadeIn()
    {
        float alpha = 1f;
        RectTransform rt = GetComponent<RectTransform>();
        rt.offsetMax = new Vector2(0, 0);
        rt.offsetMin = new Vector2(1920, 0);

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / fadeDuration;
            //image.color = new Color(0, 0, 0, alpha);
            rt.offsetMin = new Vector2(alpha * 1920, 0);

            yield return null;

        }
        rt.offsetMin = new Vector2(0, 0);
    }
    public void CallFadeInWithDelay()
    {
        StartCoroutine(Delay());

    }
    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine (FadeIn());

    }
}
