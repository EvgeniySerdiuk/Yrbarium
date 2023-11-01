using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour, IService
{
    [SerializeField] private Transform[] transformForItems;
    [SerializeField] private InteractableItem[] items;

    private List<ObjectPool<InteractableItem>> itemsPools;

    public void Init()
    {
        itemsPools = new List<ObjectPool<InteractableItem>>();
        for (int i = 0; i < items.Length; i++)
        {
            itemsPools.Add(new ObjectPool<InteractableItem>(items[i], 20, transformForItems[i]));
        }
    }

    public ObjectPool<InteractableItem> GetItemPool(ItemsType type)
    {
        return itemsPools.First(x => x.Item != null && x.Item.Type == type);
    }
}
