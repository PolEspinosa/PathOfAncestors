using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayerData(Checkpoint checkpoint)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "saveData");
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(checkpoint);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveData");
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
            return null;
        }
    }

    public static void DeleteAllData()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveData");
        File.Delete(path);
    }
}
