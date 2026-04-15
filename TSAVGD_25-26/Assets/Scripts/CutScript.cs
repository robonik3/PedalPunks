using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScript : MonoBehaviour
{
    float timer;
    [SerializeField] private float length;
    [SerializeField] private string levelName;
    [SerializeField] private GameObject skiptext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { timer = length; }
        timer+=Time.deltaTime;

        if (timer>=length)
        {
            if (levelName == "Tutorial")
            {
                SceneManager.LoadScene(levelName);
            }
            else
            {
                SceneLoader.LoadLevel(levelName);
            }
        }
        if (Input.anyKeyDown)
        {
            skiptext.SetActive(true);
        }
    }
}
