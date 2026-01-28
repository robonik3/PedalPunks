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
            SceneManager.LoadScene("Level 1");
    }
    public void loadL2() {
            SceneManager.LoadScene("Level 2");
        }
    public void loadHome()
        {
            SceneManager.LoadScene("Menu");
        }
    
}
