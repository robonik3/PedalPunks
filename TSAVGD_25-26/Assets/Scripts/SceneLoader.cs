using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadLevel(string toLevel)
    {
        SceneManager.LoadScene(toLevel);
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
    }
}
