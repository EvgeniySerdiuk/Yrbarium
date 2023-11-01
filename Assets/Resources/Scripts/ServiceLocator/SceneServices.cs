using UnityEngine;

public class SceneServices : MonoBehaviour
{
    public static ServiceLocator<IService> ServiceLocator { get; private set; }

    private void Awake()
    {
        ServiceLocator = new ServiceLocator<IService>();
        ServiceLocator.Register(GetComponentInChildren<Inventory>());
        ServiceLocator.Register(GetComponentInChildren<InventoryView>());
        ServiceLocator.Register(GetComponentInChildren<Cote>());
        ServiceLocator.Register(GetComponentInChildren<VegetableGarden>());
        ServiceLocator.Register(GetComponentInChildren<SaveAndLoadGame>());
        ServiceLocator.Register(GetComponentInChildren<Pool>());
        ServiceLocator.Register(GetComponentInChildren<Port>());
        ServiceLocator.Register(GetComponentInChildren<Kitchen>());
        ServiceLocator.Register(GetComponentInChildren<SceneController>());
    }
}
