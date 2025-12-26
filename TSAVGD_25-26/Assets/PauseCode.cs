using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCode : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
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
        Time.timeScale = 0f; //Need to fix
        isOn=true;
    }

    public void SetResume()
    {
        pauseScreen.SetActive(false);
        
        isOn=false;
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoLevel()
    {
        SceneManager.LoadScene("LevelSelect");
    }

}
