using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IService
{
    public event Action<ItemsType> ChangeAmountItem;

    private Dictionary<ItemsType, int> items = new Dictionary<ItemsType, int>();

    public void Init()
    {
        items = new Dictionary<ItemsType, int>();
        FillingInventory();
    }

    public void AddItem(InteractableItem item)
    {
        items[item.Type]++;
        ChangeAmountItem?.Invoke(item.Type);
    }

    public void RemoveItem(ItemsType type, int amount)
    {
        items[type] -= amount;
        ChangeAmountItem?.Invoke(type);
    }

    public int GetValue(ItemsType type)
    {
        return items[type];
    }

    private void FillingInventory()
    {
        var itemsType = Enum.GetValues(typeof(ItemsType));

        for (int i = 0; i < itemsType.Length; i++)
        {
            items.Add((ItemsType)itemsType.GetValue(i), 0);
        }
    }

    public Dictionary<ItemsType,int> GetSaveData()
    {
        return new Dictionary<ItemsType,int>(items);
    }

    public void LoadSaveData(List<ItemsType> itemType, List<int> amountItem)
    {
        items.Clear();
        for (int i = 0; i < itemType.Count; i++)
        {
            items.Add(itemType[i], amountItem[i]);
            ChangeAmountItem?.Invoke(itemType[i]);
        }
    }
}
