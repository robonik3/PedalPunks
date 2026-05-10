using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CutScript : MonoBehaviour
{
    float timer;
    [SerializeField] private float length;
    [SerializeField] private string levelName;
    [SerializeField] private GameObject skiptext;
    [SerializeField] private bool loadAsScene;
    [SerializeField] private bool credits;
    [SerializeField] private InputActionAsset InputActions;
    private InputAction skip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;
        skip = InputActions.FindAction("SkipCutscene");
    }

    // Update is called once per frame
    void Update()
    {
        if (skip.WasPressedThisFrame()) { timer = length; }

        timer += Time.deltaTime;

        if (timer>=length)
        {
            if (loadAsScene)
            {
                SceneManager.LoadScene(levelName,credits?LoadSceneMode.Additive:LoadSceneMode.Single);
            }
            else
            {
                SceneLoader.LoadLevel(levelName);
            }
            timer = 0;
            Destroy(gameObject);
        }
        if (Input.anyKeyDown&&skiptext)
        {
            skiptext.SetActive(true);
        }
    }
}
