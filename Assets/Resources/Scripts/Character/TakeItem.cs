using UnityEngine;

public class TakeItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {
            var item = other.GetComponent<InteractableItem>();
            item.MoveToInventory();
            SceneServices.ServiceLocator.Get<Inventory>().AddItem(item);
        }
    }

}
