using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject gasTank;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject progressBarManager;

    [SerializeField] private GameObject moveControlsText;
    [SerializeField] private GameObject attackControlsText;
    [SerializeField] private GameObject gasReminderText;
    [SerializeField] private GameObject progressReminderText;
    [SerializeField] private GameObject goodLuckText;

    private bool skipped = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(TutorialFlow());
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P)) 
        {
            SkipTutorial();
        }
    }

    void SkipTutorial()
    {
        skipped = true;

        StopAllCoroutines();

        SceneManager.LoadScene("Level 1");
    }

    IEnumerator TutorialFlow() 
    {
        yield return StartCoroutine(Tutorial());

        yield return new WaitForSeconds(10f);
        PlayerPrefs.SetInt("hasPlayedTheGame", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Level 1");
    }
    IEnumerator Tutorial()
    {

        float timer = -1;
        bool press=false;
        yield return new WaitForFixedUpdate();
        //Wait until Player has pressed inputs
        while (timer<1)
        {

            //PlayerScript.instance.fuel = 1; MADE COMMENT BECAUSE OF COMPILE ERROR
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > .1f|| Mathf.Abs(Input.GetAxis("Vertical")) > .1f)
            {
                press = true;
            }
            if (press)
            {

                timer += Time.deltaTime;
                moveControlsText.GetComponent<TextMeshProUGUI>().color = Color.Lerp(Color.white,new Color(1,1,1,0),Mathf.Clamp01(timer));
            }

            yield return null;
        }


        moveControlsText.SetActive(false);
        yield return new WaitForSecondsRealtime(1.5f);
        timer = -1;
        press = false;

        attackControlsText.SetActive(true);
        //Wait until Player tries to take down Enemy
        while (timer < 1)
        {
            //PlayerScript.instance.fuel = 1; MADE COMMENT BECAUSE OF COMPILE ERROR
            if (Input.GetKeyDown(KeyCode.C))
            {
                press = true;
            }
            if (press)
            {
                timer += Time.deltaTime;
                attackControlsText.GetComponent<TextMeshProUGUI>().color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), Mathf.Clamp01(timer));

            }

            yield return null;
        }
        attackControlsText.SetActive(false);
        yield return new WaitForSecondsRealtime(1.5f);
        timer = -1;
        press = false;

        gasReminderText.SetActive(true);
        gasTank.SetActive(true);
        yield return new WaitForSecondsRealtime(8);
        //Wait until Player tries to take down Enemy
        while (timer < 1)
        {

            timer += Time.deltaTime;
            gasReminderText.GetComponent<TextMeshProUGUI>().color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), Mathf.Clamp01(timer));

            yield return null;
        }
        gasReminderText.SetActive(false);
        yield return new WaitForSecondsRealtime(1.5f);
        timer = -1;
        press = false;

        progressReminderText.SetActive(true);
        progressBar.SetActive(true);
        progressBarManager.SetActive(true);
        yield return new WaitForSecondsRealtime(8);
        //Wait until Player tries to take down Enemy
        while (timer < 1)
        {

            timer += Time.deltaTime;
            progressReminderText.GetComponent<TextMeshProUGUI>().color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), Mathf.Clamp01(timer));

            yield return null;
        }
        progressReminderText.SetActive(false);
        goodLuckText.SetActive(true);
    }
     
}
