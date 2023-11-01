using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{
    public List<ItemsType> Type;
    public List<int> amountItem;
    public int amountSheep;
    public int amountPlaneForCarrot;

    public SaveData(Dictionary<ItemsType, int> inventory, int amountSheep, int amountPlaneForCarrot)
    {
        Type = new List<ItemsType>();
        amountItem = new List<int>();

        foreach (var item in inventory)
        {
            Type.Add(item.Key);
            amountItem.Add(item.Value);
        }

        this.amountSheep = amountSheep;
        this.amountPlaneForCarrot = amountPlaneForCarrot;
    }
}

public class SaveAndLoadGame : MonoBehaviour, IService
{
    private Inventory inventory;
    private Cote cote;
    private VegetableGarden vegetableGarden;
    private string path;

    public void Init()
    {
        inventory = SceneServices.ServiceLocator.Get<Inventory>();
        cote = SceneServices.ServiceLocator.Get<Cote>();
        vegetableGarden = SceneServices.ServiceLocator.Get<VegetableGarden>();
        path = Path.Combine(Application.persistentDataPath, "saveData.json");
        Load();
    }

    public void Save()
    {
        SaveData saveData = new SaveData(inventory.GetSaveData(), cote.GetSaveData(), vegetableGarden.GetSaveData());
        var json =  JsonUtility.ToJson(saveData);
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);

            inventory.LoadSaveData(loadedData.Type,loadedData.amountItem);
            cote.LoadSaveData(loadedData.amountSheep);
            vegetableGarden.LoadSaveData(loadedData.amountPlaneForCarrot);

            Debug.Log("Данные успешно загружены.");
        }
        else
        {
            Debug.Log("Файл сохранения не найден. Начинаем новую игру.");
        }
    }
}
