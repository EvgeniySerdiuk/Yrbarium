using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private void Start()
    {
        SceneServices.ServiceLocator.Get<Pool>().Init();
        SceneServices.ServiceLocator.Get<Inventory>().Init();
        SceneServices.ServiceLocator.Get<InventoryView>().Init();
        SceneServices.ServiceLocator.Get<SaveAndLoadGame>().Init();
        SceneServices.ServiceLocator.Get<Cote>().Init();
        SceneServices.ServiceLocator.Get<VegetableGarden>().Init();
        SceneServices.ServiceLocator.Get<Port>().Init();
        SceneServices.ServiceLocator.Get<Kitchen>().Init();
        SceneServices.ServiceLocator.Get<SceneController>().Init();
    }
}
