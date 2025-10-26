using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float levelLengthTime;
    [SerializeField] private Slider bar;
    [SerializeField] private TextMeshProUGUI bestTime;

    private bool finished;
    private float progressTimer;
    private float completionTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bestTime.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            progressTimer += Time.deltaTime;
            completionTimer += Time.unscaledDeltaTime;
            bar.value = Mathf.Clamp01(progressTimer / levelLengthTime);
            if (bar.value == 1) { EnemySpawner.instance.readyToEndLevel = true; }
        }

    }
    public void OnLevelFinish()
    {
        finished = true;
        bestTime.text = "Your Time: " + completionTimer.ToString("0.00") + System.Environment.NewLine + "Standard Time: " + levelLengthTime.ToString();
        bestTime.gameObject.SetActive(true);
    }
}
