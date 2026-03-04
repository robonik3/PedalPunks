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


/*
    void Update()
    {
        curr_level = SceneManager.GetActiveScene().name;
    }
*/

    public void loadScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }

    public void loadLevel(string sceneName)
    {
        curr_level = sceneName;

        SceneLoader.LoadLevel(sceneName);
    }
    public void loadLevelDelay(string sceneName)
    {
        IEnumerator d = Delay(sceneName);
        StartCoroutine(d);
    }
    IEnumerator Delay(string sceneName)
    {
        yield return new WaitForSecondsRealtime(1.6f);
        curr_level = sceneName;
        loadLevel(sceneName);

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
        SceneLoader.LoadLevel(curr_level);
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
