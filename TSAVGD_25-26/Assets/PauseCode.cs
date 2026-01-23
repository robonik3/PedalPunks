using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseCode : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
     //public GameObject pauseButton;
    bool isOn = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) //might need to import a package
        {
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
        //pauseButton.SetActive(false);
        Time.timeScale = 0f; //Need to fix
        isOn=true;
    }

    public void SetResume()
    {
        pauseScreen.SetActive(false);
       // pauseButton.SetActive(true);
        Time.timeScale = 1f;
        isOn=false;
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        //pauseButton.SetActive(true);
    }

    public void GoLevel()
    {
        SceneManager.LoadScene("LevelSelect");
        Time.timeScale = 1f;
        //pauseButton.SetActive(true);
    }

}
