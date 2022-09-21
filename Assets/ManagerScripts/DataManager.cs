using System.Collections;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public static class DataManager 
{
   public static void savePlayer(SlikyController player, Gun playerGun, GameObject playerObj)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "AlienGoesHome.game";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player, playerGun, playerObj);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData loadPlayer()
    {
        string path = Application.persistentDataPath + "AlienGoesHome.game";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }    
    }
}
