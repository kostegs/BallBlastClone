using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadSystem
{
    public static void SaveData()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameData.sav";
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

        SaveLoadSystemData data = new SaveLoadSystemData();
        data.FillDataFromDataStorage();

        binaryFormatter.Serialize(fs, data);
        fs.Close();
    }

    public static SaveLoadSystemData LoadData()
    {
        string path = Application.persistentDataPath + "/GameData.sav";

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            SaveLoadSystemData data = binaryFormatter.Deserialize(fs) as SaveLoadSystemData;
            fs.Close();

            return data;
        }
        else { return null; }
    }

    public static bool SaveFileExists()
    {
        string path = Application.persistentDataPath + "/GameData.sav";

        return (File.Exists(path));
    }
}
