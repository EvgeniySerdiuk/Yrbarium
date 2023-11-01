using System.Collections;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [Header("Interactable item")]
    [SerializeField] private InteractableItem interactableItem; // Интерактивный итем который добывается.
    [SerializeField] private float itemGenerationTime; // Время генерации интерактивного итема

    [Header("Container for items")]
    [SerializeField] private Transform container; // Контейнер для добытых итемов.
    [SerializeField,Range(0,5)] private float itemSpacing; // Расстояние между предметами.
    [SerializeField] private Vector3 offset; // Смещение.

    [Header("Mining Object")]
    [SerializeField] private MiningObj miningPrefab; // Объект который добывает итем.
    [SerializeField] private Transform spawnPoint; // Точка спавна.
    [SerializeField] private int priceMiningObj; // Стоимость добывающего объекта.
    [SerializeField] private int amountItemProduction; // Количество добываемых итемов с одного добывающего объекта.
    [SerializeField] private BuyPanel buyPanel; // Панель для покупки добывающего объекта.

    protected ObjectPool<MiningObj> miningObjPool;
    protected ObjectPool<InteractableItem> itemsPool;
    protected ItemsContainer itemsContainer;
    protected int amountMiningObjects = 0;
    private Inventory inventory;

    public virtual void Init()
    {
        miningObjPool = new ObjectPool<MiningObj>(miningPrefab, 5, spawnPoint);
        itemsPool = SceneServices.ServiceLocator.Get<Pool>().GetItemPool(interactableItem.Type);
        itemsContainer = new ItemsContainer(container, interactableItem, itemSpacing, offset);
        if (amountMiningObjects == 0) amountMiningObjects++;
        inventory = SceneServices.ServiceLocator.Get<Inventory>();
        StartCoroutine(GenerateItem());
    }

    private void OnEnable()
    {
        buyPanel.IsBuy += BuyMiningObj;
    }

    private void OnDisable()
    {
        buyPanel.IsBuy -= BuyMiningObj;
    }

    private void BuyMiningObj()
    {
        if (inventory.GetValue(ItemsType.Coin) >= priceMiningObj)
        {
            amountMiningObjects++;
            inventory.RemoveItem(ItemsType.Coin, priceMiningObj);
            CreateMiningObj();
        }
    }

    protected virtual void CreateMiningObj()
    {

    }

    private IEnumerator GenerateItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(itemGenerationTime);
            int generatedMeat = amountMiningObjects * amountItemProduction;

            for (int i = 0; i < generatedMeat; i++)
            {
                var point = itemsContainer.GetPoint();
                if (point == null) continue;
                var item = itemsPool.Get();
                item.transform.SetParent(point, true);
                item.transform.position = point.position;
            }
        }
    }

    public int GetSaveData()
    {
        return amountMiningObjects;
    }

    public void LoadSaveData(int amount)
    {
        amountMiningObjects = amount;
    }
}
