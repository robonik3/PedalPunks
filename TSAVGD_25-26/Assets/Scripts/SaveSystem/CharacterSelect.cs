using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterSelect : MonoBehaviour
{
    public int selectedCharacter;
    public int selectedBike;
    public bool[] unlockedCharacters;

    public PlayerType[] characterList;
    public BikeType[] bikeList;

    [SerializeField] private Image PlayerPreview;
    [SerializeField] private Image BikePreview;
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private TextMeshProUGUI BikeName;
    // 0:Tony / 1:Enemy / 2:Astronaut / 3:ScooterMan / 4:Vampire / 5:Aviator

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            data.unlockedCharacters.CopyTo(unlockedCharacters, 0);
            ChangePlayerCharacter(data.selectedCharacter);
            ChangeBike(data.selectedBike);
        }

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

            if (unlockedCharacters[selectedBike]) SaveSystem.SavePlayer(this);

        }
        else 
        { 
            PlayerPreview.color = Color.black;
            CharacterName.text = "???";

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

            if (unlockedCharacters[selectedCharacter]) SaveSystem.SavePlayer(this);
        }
        else
        {
            BikePreview.color = Color.black;
            BikeName.text = "???";
        }


    }
}
