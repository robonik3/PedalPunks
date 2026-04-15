using System.Collections;
using UnityEngine;

public class ExplosionAudioScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float shakeDuration = 0.2f;
    private float shakeStrength = 0.2f;
    Transform cam;
    Vector3 camOriginalPosition;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.volume *= AudioPlayer.instance.SFXvolume;
        audioSource.Play();

        cam = Camera.main.transform;
        camOriginalPosition = cam.localPosition;

        StartCoroutine(ShakeCamera());
    }

    void Start()
    {
        Destroy(gameObject, audioSource.clip.length);
    }
    IEnumerator ShakeCamera()
    {
        float elapsed = 0f;
        while (elapsed< shakeDuration)
        {
            cam.localPosition = camOriginalPosition + (Vector3)Random.insideUnitCircle * shakeStrength;

            elapsed += Time.deltaTime;
            yield return null;
        }

        //cam.localPosition = camOriginalPosition;
        cam.position = new Vector3(0, 0, -10);
    }
}
