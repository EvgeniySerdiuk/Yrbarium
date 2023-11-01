using System.Linq;
using UnityEngine;

public class VegetableGarden : Farm, IService
{
    [SerializeField] private Transform[] slotsForMiningObj;

    public override void Init()
    {
        base.Init();

        for (int i = 0; i < amountMiningObjects - 1; i++)
        {
            CreateMiningObj();
        }
    }

    protected override void CreateMiningObj()
    {
        var slot = slotsForMiningObj.FirstOrDefault(x => x.childCount == 0);
        if (slot == null) return;

        var obj = miningObjPool.Get();
        obj.transform.SetParent(slot, false);
        itemsContainer.AddContainer(obj.transform);
    }
}
