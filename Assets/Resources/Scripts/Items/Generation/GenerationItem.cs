using System.Collections;
using UnityEngine;

public class GenerationItem
{
    private float generationTime;
    private int sheepCount;
    private int multiplier;
    private ObjectPool<InteractableItem> interactableItemsPool;

    public GenerationItem(ref int sheepCount,float generationTime, int multiplier, InteractableItem item, Transform spawnPoint, MonoBehaviour mono)
    {
        this.sheepCount = sheepCount;
        this.generationTime = generationTime;
        this.multiplier = multiplier;
        interactableItemsPool = new ObjectPool<InteractableItem>(item,15,spawnPoint);
        mono.StartCoroutine(GenerateMeat());
    }

    public IEnumerator GenerateMeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(generationTime);

            int generatedMeat = sheepCount * multiplier;
            Debug.Log(sheepCount);

            for (int i = 0; i < generatedMeat; i++)
            {
                interactableItemsPool.Get();
            }
        }
    }
}
