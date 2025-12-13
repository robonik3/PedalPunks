using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class buttonHandler : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame

    public void loadCredits() {
            SceneManager.LoadScene("Credits");
    }
    public void loadLevels() {
            SceneManager.LoadScene("LevelSelect");
        }

    
}
