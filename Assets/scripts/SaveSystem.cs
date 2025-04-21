using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(Data data) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.json";
        FileStream stream = new FileStream(path, FileMode.Create);

        UserData newData = new UserData(data);

        formatter.Serialize(stream, newData);
        stream.Close();
    }

    public static UserData LoadData()
    {
        string path = Application.persistentDataPath + "/data.json";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            UserData data = formatter.Deserialize(stream) as UserData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found" + path);
            return null;
        }
    }
}
