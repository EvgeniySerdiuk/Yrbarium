using System.Linq;
using UnityEngine;

public class InventoryView : MonoBehaviour,IService
{
    [SerializeField] private InventorySlotView[] inventorySlotsView;
    
    private Inventory inventory;

    public void Init()
    {
        inventory = SceneServices.ServiceLocator.Get<Inventory>();

        foreach (var slot in inventorySlotsView)
        {
            slot.Init();
            slot.SetAmount(inventory.GetValue(slot.ViewType));
        }

        inventory.ChangeAmountItem += SetValue;
    }

    public void SetValue(ItemsType type)
    {
        inventorySlotsView.First(x => x.ViewType == type).SetAmount(inventory.GetValue(type));
    }

    public Vector3 GetPointToMove(ItemsType type)
    {
        return inventorySlotsView.First(x=> x.ViewType == type).transform.position;
    }
}
