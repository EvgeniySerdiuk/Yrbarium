using System.Collections;
using UnityEngine;

public class Port : MonoBehaviour,IService
{
    [SerializeField] private InteractableItem miningItem;
    [SerializeField] private float miningTime;
    [SerializeField] private Transform pointToSpawn;
    [SerializeField] private DetectedCharacter detected;

    private Inventory inventory;
    private ObjectPool<InteractableItem> pool;

    
    public void Init()
    {
        inventory = SceneServices.ServiceLocator.Get<Inventory>();
        pool = SceneServices.ServiceLocator.Get<Pool>().GetItemPool(miningItem.Type);
        detected.IsCharacterHere += OnOffFishing;
    }


    private void OnOffFishing(bool value)
    {
        Debug.Log("Пришел");
        if(value)
        {
            StartCoroutine(Fishing());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    IEnumerator Fishing()
    {
        while (true)
        {
            yield return new WaitForSeconds(miningTime);
            var minigObj = pool.Get();
            minigObj.transform.position = pointToSpawn.position;
            minigObj.MoveToInventory();
            inventory.AddItem(minigObj);
        }
    }
}
