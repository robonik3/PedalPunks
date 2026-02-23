using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScript : MonoBehaviour
{
    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
      timer+=Time.deltaTime;
      if (timer>=39f)
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
}
