using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SavePlayer(CharacterSelect player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.s";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

    }
    public static void DeleteSave()
    {
        string path = Application.persistentDataPath + "/player.s";
        if (File.Exists(path))
        {
            Debug.Log("Your File is Deleted... FOREVER!!!! MWAHAHAHA-A-HA-- *cough");
            File.Delete(path);
        }
    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.s";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("NO SAVE DUBLASS: check your wallet pocket titled " + path);
            return null;
        }
    }

}
