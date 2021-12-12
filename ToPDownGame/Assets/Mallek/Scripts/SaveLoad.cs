using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad 
{
    public static void save(Singleton singleton)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player4.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        GeneralPlayerData data = new GeneralPlayerData(singleton);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GeneralPlayerData load()
    {
        string path = Application.persistentDataPath + "/player4.data";
        Debug.Log(Application.persistentDataPath );
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GeneralPlayerData data = formatter.Deserialize(stream) as GeneralPlayerData;
            stream.Close();
            return data;
        }
        else
        {
            save(Singleton._instance);
            Debug.LogError("save file not found");
            return null;
        }
    }
}
