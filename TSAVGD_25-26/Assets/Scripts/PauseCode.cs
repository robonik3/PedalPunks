using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseCode : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject pauseButton;
    public static bool isOn = false;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            Debug.Log("P Pressed");
            if(isOn)
            {
                SetResume();
            }
            else
            {
                SetPause();
            }
        }
    }

    public void SetPause()
    {
        pauseScreen.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        Debug.Log("Time.deltaTime: " + Time.deltaTime);
        isOn=true;
    }

    public void SetResume()
    {
        pauseScreen.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        isOn=false;
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        //pauseButton.SetActive(true);
        isOn = false;
    }

    public void GoLevel()
    {
        SceneManager.LoadScene("LevelSelect");
        Time.timeScale = 1f;
        //pauseButton.SetActive(true);
        isOn = false;
    }

}
