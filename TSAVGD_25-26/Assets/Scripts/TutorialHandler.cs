using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset InputActions;
    [SerializeField] private GameObject gasTank;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject progressBarManager;
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject enemyspawner;

    [SerializeField] private GameObject moveControlsText;
    [SerializeField] private GameObject attackControlsText;
    [SerializeField] private GameObject gasReminderText;
    [SerializeField] private GameObject progressReminderText;
    [SerializeField] private GameObject goodLuckText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        StartCoroutine(TutorialFlow());
    }
    void Update()
    {
    }

    void SkipTutorial()
    {

        StopAllCoroutines();

        SceneManager.LoadScene("Level 1");
        buttonHandler.curr_level = "Level 1";
    }

    IEnumerator TutorialFlow() 
    {
        yield return StartCoroutine(Tutorial());

    }
    IEnumerator Tutorial()
    {
        InputAction move = InputActions.FindAction("Movement");
        InputAction punch = InputActions.FindAction("Attack");
        InputAction jump = InputActions.FindAction("Trick");
        InputAction ability = InputActions.FindAction("Accelerate");
        float timer = -1;
        bool press=false;
        yield return new WaitForFixedUpdate();
        //Wait until Player has pressed inputs
        while (timer<1)
        {

            PlayerScript.instance.fuel = 1;
            if (move.ReadValue<Vector2>().magnitude>0 || punch.WasPressedThisFrame() || jump.WasPressedThisFrame()||ability.WasPressedThisFrame())
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
        Instantiate(EnemyPrefab, new Vector3(-10, 0, 0), new Quaternion());
        attackControlsText.SetActive(true);
        //Wait until Player tries to take down Enemy
        while (timer < 1)
        {
            PlayerScript.instance.fuel = 1;

            if (punch.WasPerformedThisFrame())
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
        PlayerScript.instance.fuel = 1;

        timer = -1;
        press = false;

        gasReminderText.SetActive(true);
        gasTank.SetActive(true);

        yield return new WaitForSecondsRealtime(8);
        Instantiate(EnemyPrefab, new Vector3(-10, 0, 0), new Quaternion());

        //Wait until Player tries to take down Enemy
        while (timer < 1)
        {

            timer += Time.deltaTime;
            gasReminderText.GetComponent<TextMeshProUGUI>().color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), Mathf.Clamp01(timer));

            yield return null;
        }
        gasReminderText.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        timer = -1;
        press = false;

        progressReminderText.SetActive(true);
        progressBar.SetActive(true);
        progressBarManager.SetActive(true);
        enemyspawner.SetActive(true);
        yield return new WaitForSecondsRealtime(9);
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
