using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionData : MonoBehaviour
{
    public static TransitionData instance;
    public bool[] newUnlocks;
    [SerializeField] private string fromLevel; 
    [SerializeField] private string toLevel; 
    [SerializeField] private GameObject UnlockDisplay;
    [SerializeField] private GameObject AlternateWinText;
    public float standardTime;
    public float yourTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SceneManager.sceneLoaded += Reload;
        Reload(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        

    }
    public void Reload(Scene n, LoadSceneMode lsm)
    {
        if (instance != null && instance != this)
        {
            SceneManager.sceneLoaded -= Reload;

            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        if (n.name==fromLevel || n.name == "Transition")
        {
            if(n.name == "Transition")
            {
                StartCoroutine("Buffer");
            }

        }
        else
        {
            SceneManager.sceneLoaded -= Reload;

            instance = null;
            Destroy(gameObject);

        }
    }
    IEnumerator Buffer()
    {
        yield return new WaitForSecondsRealtime(.1f);
        UnlockDisplay.transform.SetParent(FindAnyObjectByType<Canvas>().transform,false);
        UnlockDisplay.transform.SetAsFirstSibling();
        AlternateWinText.transform.SetParent(FindAnyObjectByType<Canvas>().transform,false);
        AlternateWinText.transform.SetAsFirstSibling();
        buttonHandler bh = FindFirstObjectByType<buttonHandler>();
        GameObject.Find("Next Button").GetComponent<Button>().onClick.AddListener(() => bh.loadSceneDelay(toLevel));
        GameObject.Find("Next Button").GetComponent<Button>().onClick.AddListener(() => RemoveInstance());
        bool du = FindFirstObjectByType<CharacterSelect>().UnlockNewCharacters(newUnlocks);
        yield return new WaitForSecondsRealtime(2);
        if (du) 
        { 
            UnlockDisplay.SetActive(true);
            UnlockDisplay.GetComponent<RectTransform>().offsetMin = Vector3.zero;
            UnlockDisplay.GetComponent<RectTransform>().offsetMax = Vector3.zero;
        }
        else
        {
            AlternateWinText.SetActive(true);
        }

    }
    public void RemoveInstance()
    {
        SceneManager.sceneLoaded -= Reload;

        instance = null;
        Destroy(gameObject);
    }
}
