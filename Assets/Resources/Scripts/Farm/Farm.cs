using System.Collections;
using UnityEngine;

public class Farm : MonoBehaviour
{
    [Header("Interactable item")]
    [SerializeField] private InteractableItem interactableItem; // ������������� ���� ������� ����������.
    [SerializeField] private float itemGenerationTime; // ����� ��������� �������������� �����

    [Header("Container for items")]
    [SerializeField] private Transform container; // ��������� ��� ������� ������.
    [SerializeField,Range(0,5)] private float itemSpacing; // ���������� ����� ����������.
    [SerializeField] private Vector3 offset; // ��������.

    [Header("Mining Object")]
    [SerializeField] private MiningObj miningPrefab; // ������ ������� �������� ����.
    [SerializeField] private Transform spawnPoint; // ����� ������.
    [SerializeField] private int priceMiningObj; // ��������� ����������� �������.
    [SerializeField] private int amountItemProduction; // ���������� ���������� ������ � ������ ����������� �������.
    [SerializeField] private BuyPanel buyPanel; // ������ ��� ������� ����������� �������.

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
