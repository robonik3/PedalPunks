using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class buttonHandler : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    [SerializeField] GameObject pauseCanvas;

    public void loadCredits() {
            SceneManager.LoadScene("Credits");
    }
    public void loadLevels() {
            SceneManager.LoadScene("LevelSelect");
        }
    public void loadL1() {
        bool tutorialCompleted = PlayerPrefs.GetInt("hasPlayedTheGame", 0) == 1; // makes sure tutorial has been played

        if(!tutorialCompleted)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else { SceneManager.LoadScene("Level 1"); }
            
    }
    public void loadL2() {
         SceneManager.LoadScene("Level 2");
    }
    public void loadHome()
        {
            SceneManager.LoadScene("Menu");
        }
    
}
