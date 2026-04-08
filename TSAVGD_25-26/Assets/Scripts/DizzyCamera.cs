using System.Collections;
using UnityEngine;


public class DizzyCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartTiltUp()
    {
        StartCoroutine(Tilt(-30));
    }
    public void StartTiltDown()
    {
        StartCoroutine(Tilt(30));
    }
    IEnumerator Tilt(int updown)
    {
        float timer = 0;
        float ogzrot = transform.eulerAngles.z;

        while (timer < 1)
        {
            timer += Time.deltaTime/3;
            transform.eulerAngles = new Vector3(0, 0, ogzrot + (timer < 0.5 ? 4 * Mathf.Pow(timer,3): 1 - Mathf.Pow(-2 * timer + 2, 3) / 2) *updown);
            yield return null;
        }
        transform.eulerAngles = new Vector3(0, 0,ogzrot + updown);

    }

}
