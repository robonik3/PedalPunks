using System.Collections;
using UnityEngine;
using TMPro;
public class TransitionAnimation : MonoBehaviour
{
    public static TransitionAnimation instance;
    [SerializeField] private Animator driver;
    [SerializeField] private Animator playeranim;
    [SerializeField] private Animator bikeanim;
    [SerializeField] private TextMeshProUGUI bestTime;
    public bool end;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        PlayerData loaded = SaveSystem.LoadPlayer();
        CharacterSelect c = FindFirstObjectByType<CharacterSelect>();
        if(c.characterList[loaded.selectedCharacter].visualT!=null) playeranim.runtimeAnimatorController = c.characterList[loaded.selectedCharacter].visualT;
        bikeanim.runtimeAnimatorController = c.bikeList[loaded.selectedBike].visualT;
        bestTime.text = "Your Time: " + TransitionData.instance.yourTime.ToString("0.00") + System.Environment.NewLine + "Standard Time: " + TransitionData.instance.standardTime.ToString();

        StartCoroutine("Cutscene");
    }
    public void SetEndToTrue()
    {
        end = true;
    }
    IEnumerator Cutscene()
    {
        driver.Play("DriveIn");

        yield return new WaitForSecondsRealtime(1);

        while (!end)
        {
            yield return null;
        }
        
        //playeranim.Play("Wheelie");
        //bikeanim.Play("Wheelie");

        yield return new WaitForSecondsRealtime(.5f);
        driver.Play("DriveOut");

    }
}
