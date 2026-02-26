using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Timeline;
using Unity.VisualScripting;

public class CharacterInfo : MonoBehaviour
{
    public int selectedCharacter;
    public int selectedBike;
    public bool[] unlockedCharacters;

    public PlayerType[] characterList;
    public BikeType[] bikeList;
    public Text description;

    [SerializeField] private Image PlayerPreview;
    [SerializeField] private Image BikePreview;
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private TextMeshProUGUI BikeName;

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
        if(selectedCharacter > 5) {  selectedCharacter = 0; }
        if(selectedCharacter < 0) {  selectedCharacter = 5; }

        PlayerPreview.sprite = characterList[selectedCharacter].DisplaySprite;
        
        if (unlockedCharacters[selectedCharacter]) 
        { 
            PlayerPreview.color = Color.white;
            CharacterName.text = characterList[selectedCharacter].characterName;

            if (unlockedCharacters[selectedBike]);
                switch (characterList[selectedCharacter].characterName)
                    {

                        case "Biker":
            description.text = "Basic enemy that drives up close to you. Once close enough, it wheelies and tries to hit the front of your bike to sabotage it.";
                            break;

                        case "Astronaut":
            description.text = "This enemy uses its powerful jet engine to drive forward extremely fast, destroying everything in its path. However, it can only travel in one direction";
                            break;

                        case "ScooterMan":
            description.text = "Drives above or below you, eventually charging to your side to attack. Despite its easy to dodge attack, this enemy takes five hits to knock off. ";
                            break;

                        case "Aviator":
                            description.text = "???";
                            break;

                        case "Bike Coffin":
            description.text = "Dispenses potholes on the road, which steal your fuel.";
                            break;

                    }
        }
        else 
        { 
            PlayerPreview.color = Color.black;
            CharacterName.text = "???";
            description.text = "???";

        }


    }
    public void ChangeBike(int i)
    {
        selectedBike = i;
        if(selectedBike > 5) {  selectedBike = 0; }
        if(selectedBike < 0) {  selectedBike = 5; }

        BikePreview.sprite = bikeList[selectedBike].DisplaySprite;

        if (unlockedCharacters[selectedBike])
        {
            BikePreview.color = Color.white;
            BikeName.text = bikeList[selectedBike].bikeName;

            if (unlockedCharacters[selectedCharacter]); //SaveSystem.SavePlayer(this);
                            switch (characterList[selectedCharacter].characterName)
                    {

                        case "Biker":
            description.text = "Basic enemy that drives up close to you. Once close enough, it wheelies and tries to hit the front of your bike to sabotage it.";
                            break;

                        case "Astronaut":
            description.text = "This enemy uses its powerful jet engine to drive forward extremely fast, destroying everything in its path. However, it can only travel in one direction";
                            break;

                        case "ScooterMan":
            description.text = "Drives above or below you, eventually charging to your side to attack. Despite its easy to dodge attack, this enemy takes five hits to knock off. ";
                            break;

                        case "Aviator":
                            description.text = "???";
                            break;

                        case "Bike Coffin":
            description.text = "Dispenses potholes on the road, which steal your fuel.";
                            break;

                    }
        }
        else
        {
            BikePreview.color = Color.black;
            BikeName.text = "???";
            description.text = "???";
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
          
        }
        return displayNewUnlocks;
    }
}
