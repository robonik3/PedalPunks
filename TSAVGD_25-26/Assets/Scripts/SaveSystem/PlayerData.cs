using System;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class PlayerData
{
    public bool[] unlockedCharacters;
    public int selectedCharacter;
    public int selectedBike;
    public PlayerData(CharacterSelect player)
    {
        unlockedCharacters = new bool[6];
        player.unlockedCharacters.CopyTo(unlockedCharacters,0);
        selectedCharacter = player.selectedCharacter;
        selectedBike = player.selectedBike;
    }
}
