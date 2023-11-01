using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T itemPrefab;
    private List<T> itemsPool;
    private Transform transformParent;

    public InteractableItem Item {get; private set;}

    public ObjectPool(T itemPrefab, int amountItem, Transform transformParent)
    {
        this.itemPrefab = itemPrefab;
        itemsPool = new List<T>();
        this.transformParent = transformParent;

        if(itemPrefab.TryGetComponent<InteractableItem>(out InteractableItem item))
        {
            Item = item;
        }

        for (int i = 0; i < amountItem; i++)
        {
            var obj = GameObject.Instantiate(this.itemPrefab, this.transformParent);
            obj.gameObject.SetActive(false);
            itemsPool.Add(obj);
        }
    }

    public void Remove(T obj)
    {
        obj.transform.parent = transformParent;
        obj.gameObject.SetActive(false);
    }

    public T Get()
    {
        var obj = itemsPool.FirstOrDefault(x => !x.isActiveAndEnabled);

        if (obj == null)
        {
            obj = Create();
        }
        obj.gameObject.SetActive(true);

        return obj;
    }

    private T Create()
    {
        var obj = GameObject.Instantiate(itemPrefab, transformParent);
        itemsPool.Add(obj);
        return obj;
    }
}
