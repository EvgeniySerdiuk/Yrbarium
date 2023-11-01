using System.Collections;
using UnityEngine;

public class MarketStand : MonoBehaviour
{
    [Header("Buy item info")]
    [SerializeField] private InteractableItem buyItem;
    [SerializeField] private SpriteRenderer marketImage;
    [SerializeField] private int price;

    [Header("Mining obj info")]
    [SerializeField] private InteractableItem money;
    [SerializeField] private Transform spawnPoint;

    private Inventory inventory;

    private void Start()
    {
        marketImage.sprite = buyItem.ItemSprite;
        inventory = SceneServices.ServiceLocator.Get<Inventory>();
    }

    private void OnTriggerEnter()
    {
        StartCoroutine(Buy());
    }

    private void OnTriggerExit()
    {
        StopAllCoroutines();
    }

    IEnumerator Buy()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f);
            if (inventory.GetValue(buyItem.Type) <= 0) yield break;
            SceneServices.ServiceLocator.Get<Pool>().GetItemPool(buyItem.Type).Get().MoveFromInventory(marketImage.transform.position);
            inventory.RemoveItem(buyItem.Type, 1);
            Payment();
        }
    }

    private void Payment()
    {
        var obj = SceneServices.ServiceLocator.Get<Pool>().GetItemPool(money.Type).Get();
        obj.transform.position = spawnPoint.position;
        obj.MoveToInventory();
        inventory.AddItem(money);
    }
}
