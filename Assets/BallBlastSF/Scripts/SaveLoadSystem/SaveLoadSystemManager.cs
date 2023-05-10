using UnityEngine;

public class SaveLoadSystemManager : MonoBehaviour
{
    public void SaveGame() => SaveLoadSystem.SaveData();

    public void LoadGame() 
    { 
        SaveLoadSystemData data = SaveLoadSystem.LoadData();
        DataStorage.LoadDataFromSave(data);        
    } 
}
