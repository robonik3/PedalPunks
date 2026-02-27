using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class CharacterSelect : MonoBehaviour
{
    public int selectedCharacter;
    public int selectedBike;
    public bool[] unlockedCharacters;
    public bool info = false;

    public PlayerType[] characterList;
    public BikeType[] bikeList;

    [SerializeField] private Image PlayerPreview;
    [SerializeField] private Image BikePreview;
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private TextMeshProUGUI BikeName;

    [SerializeField] private TextMeshProUGUI EnemyDescription;
    [SerializeField] private TextMeshProUGUI BikeDescription;

    [SerializeField] private bool dontLoad;
    // 0:Tony / 1:Enemy / 2:Astronaut / 3:ScooterMan / 4:Vampire / 5:Aviator

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            data.unlockedCharacters.CopyTo(unlockedCharacters, 0);
            if (!dontLoad)
            {
                ChangePlayerCharacter(data.selectedCharacter);
                ChangeBike(data.selectedBike);
            }
            else
            {
                ChangePlayerCharacter(0);
                ChangeBike(0);
            }

        }

    }
    public void FactoryReset()
    {
        for(int i = 0; i < unlockedCharacters.Length; i++)
        {
            unlockedCharacters[i] = false;
        }
        unlockedCharacters[0] = true;
        ChangePlayerCharacter(0);
        ChangeBike(0);
    }
    public void NextCharacter() { ChangePlayerCharacter(selectedCharacter + 1); }
    public void PreviousCharacter() { ChangePlayerCharacter(selectedCharacter - 1); } 
    public void NextBike() { ChangeBike(selectedBike + 1); }
    public void PreviousBike() { ChangeBike(selectedBike - 1); }
    public void ChangePlayerCharacter(int i)
    {
        selectedCharacter = i;
        if(selectedCharacter > characterList.Length-1) {  selectedCharacter = 0; }
        if(selectedCharacter < 0) {  selectedCharacter = characterList.Length-1; }

        PlayerPreview.sprite = characterList[selectedCharacter].DisplaySprite;
        
        if (unlockedCharacters[selectedCharacter]) 
        { 
            PlayerPreview.color = Color.white;
            CharacterName.text = characterList[selectedCharacter].characterName;
            if (info)
            {
                EnemyDescription.text = characterList[selectedCharacter].Description;
            }
            else
            {
                if (unlockedCharacters[selectedBike]) SaveSystem.SavePlayer(this);
            }


        }
        else 
        { 
            PlayerPreview.color = Color.black;
            CharacterName.text = "???";
            if (info)
            {
                EnemyDescription.text = "???";
            }

        }


    }
    public void ChangeBike(int i)
    {
        selectedBike = i;
        if(selectedBike > bikeList.Length-1) {  selectedBike = 0; }
        if(selectedBike < 0) {  selectedBike = bikeList.Length-1; }

        BikePreview.sprite = bikeList[selectedBike].DisplaySprite;

        if (unlockedCharacters[selectedBike])
        {
            BikePreview.color = Color.white;
            BikeName.text = bikeList[selectedBike].bikeName;

             if (info)
            {
                BikeDescription.text = bikeList[selectedBike].Description;
            }
            else
            {
                if (unlockedCharacters[selectedCharacter]) SaveSystem.SavePlayer(this);

            }
        }
        else
        {
            BikePreview.color = Color.black;
            BikeName.text = "???";
            if (info) {
            BikeDescription.text="???";
            }
        }


    }
    public bool UnlockNewCharacters(bool[] newUnlocks)
    {
        bool displayNewUnlocks=false;

        for (int i=0; i < unlockedCharacters.Length; i++)
        {
            if ( !unlockedCharacters[i] && newUnlocks[i])
            {
                unlockedCharacters[i] = true;
                displayNewUnlocks = true;
            }
        }

        if (displayNewUnlocks)
        {
            SaveSystem.SavePlayer(this);
        }
        return displayNewUnlocks;
    }
}
