using System.Collections;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [SerializeField] private ItemsType type;
    [SerializeField] private Sprite itemSprite;

    public ItemsType Type => type;
    public Sprite ItemSprite => itemSprite;

    public void MoveToInventory()
    {
        var Uiposition = SceneServices.ServiceLocator.Get<InventoryView>().GetPointToMove(type);
        Uiposition.z = 4;
        StartCoroutine(MoveItem(Camera.main.ScreenToWorldPoint(Uiposition)));
    }

    public void MoveFromInventory(Vector3 target)
    {
        var Uiposition = SceneServices.ServiceLocator.Get<InventoryView>().GetPointToMove(type);
        Uiposition.z = 4;
        transform.position = Camera.main.ScreenToWorldPoint(Uiposition);
        StartCoroutine(MoveItem(target));
    }

    IEnumerator MoveItem(Vector3 target)
    {
        while (Vector3.Distance(transform.position,target)> 0.2f)
        {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.MoveTowards(transform.position, target, 10 * Time.deltaTime);
        }
        SceneServices.ServiceLocator.Get<Pool>().GetItemPool(type).Remove(this);
    }
}
