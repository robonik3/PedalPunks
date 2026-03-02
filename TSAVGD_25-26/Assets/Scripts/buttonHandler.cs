using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class buttonHandler : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    //stores current level name
    public static string curr_level= "Level 1";

    // Update is called once per frame
    [SerializeField] GameObject pauseCanvas;
/*
    void Update()
    {
        curr_level = SceneManager.GetActiveScene().name;
    }
*/
    public void loadCredits() {
            SceneManager.LoadScene("Credits");
    }
    public void loadLevels() {
            SceneManager.LoadScene("LevelSelect");
        }
    public void loadL1() 
    {
         SceneManager.LoadScene("Level 1");
         curr_level = "Level 1";
         Debug.Log(curr_level);
            
    }
    public void loadL2() {
         SceneManager.LoadScene("Level 2");
         curr_level = "Level 2";
         Debug.Log(curr_level);
    }
    public void loadL3()
    {
        SceneManager.LoadScene("Level 3");
        curr_level = "Level 3";
        Debug.Log(curr_level);
    }
    public void loadHome()
        {
            SceneManager.LoadScene("Menu");
        }
    public void loadScene(string sceneName)
        {
            if (sceneName == "Tutorial")
        {
            SceneManager.LoadScene("Cutscene1");
        } else {
            SceneManager.LoadScene(sceneName);
            curr_level = sceneName;
            Debug.Log(sceneName + " loaded");
        }
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
        curr_level = sceneName;
        Debug.Log(curr_level);

    }
    public void DeleteSaveData()
    {
        SaveSystem.DeleteSave();
    }
    public void SaveSettings()
    {
        AdjustSettings data = FindFirstObjectByType<AdjustSettings>();
        SaveSystem.SaveSettings(data);
        AudioPlayer.instance.SFXvolume = data.SFXVolume;
        AudioPlayer.instance.MusicVolume = data.MusicVolume;
    }
    public void Restart()
    {
        SceneManager.LoadScene(curr_level);
        Debug.Log(curr_level + " Restarted");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void CameraReset()
    {
        Camera.main.transform.position = Vector3.zero;
        Camera.main.orthographicSize = 5;
    }
    
}
