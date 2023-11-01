using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsContainer
{
    private Transform parentTransform;
    private float itemSpacing;
    private Bounds planeBound;
    private Bounds itemBound;
    private Vector3 spawnPosition;
    private GameObject pointObject;
    private Vector3 offset;
    private List<Transform> points = new List<Transform>();

    public ItemsContainer(Transform parentForItem, InteractableItem item, float itemSpacing, Vector3 offset) 
    { 
        parentTransform = parentForItem;
        planeBound = parentTransform.GetComponent<Renderer>().bounds;
        itemBound = item.GetComponent<Renderer>().bounds;
        this.itemSpacing = itemSpacing;
        this.offset = offset;
        SpawnPoints();
    }

    private void SpawnPoints()
    {
        pointObject = new GameObject("Point");

        spawnPosition = planeBound.min - itemBound.min;
        spawnPosition.y = planeBound.max.y + itemBound.max.y/2;

        while (spawnPosition.x <= planeBound.max.x)
        {
            var obj = GameObject.Instantiate(pointObject, spawnPosition + offset, Quaternion.identity,parentTransform);
            points.Add(obj.transform);
            spawnPosition.z += itemBound.size.z + itemSpacing;

            if (spawnPosition.z > planeBound.max.z)
            {
                spawnPosition.z = planeBound.min.z;
                spawnPosition.x += itemBound.size.x + itemSpacing;
            }
        }

        GameObject.Destroy(pointObject);
    }

    public void AddContainer(Transform parentForItem)
    {
        parentTransform = parentForItem;
        planeBound = parentTransform.GetComponent<Renderer>().bounds;
        SpawnPoints();
    }

    public Transform GetPoint()
    {
        return points.FirstOrDefault(x => x.childCount == 0);
    }
}
