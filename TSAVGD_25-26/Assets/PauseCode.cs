using UnityEngine;

public class PauseCode : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;

    public void SetPause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void SetResume()
    {
        pauseScreen.SetActive(false);
    }


}
