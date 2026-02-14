using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public void loadL1() 
    {
         SceneManager.LoadScene("Level 1");
            
    }
    public void loadL2() {
         SceneManager.LoadScene("Level 2");
    }
    public void loadL3()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void loadHome()
        {
            SceneManager.LoadScene("Menu");
        }
    public void loadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    public void loadSceneDelay(string sceneName)
    {
        IEnumerator d = Delay(sceneName);
        StartCoroutine(d);
    }
    IEnumerator Delay(string sceneName)
    {
        yield return new WaitForSecondsRealtime(1.6f);
        SceneManager.LoadScene(sceneName);

    }
    public void DeleteSaveData()
    {
        SaveSystem.DeleteSave();
    }
}
