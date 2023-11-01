public class Cote : Farm, IService
{
    public override void Init()
    {
        base.Init();

        for (int i = 0; i < amountMiningObjects;i++)
        {
          miningObjPool.Get();
        }
    }

    protected override void CreateMiningObj()
    {
        miningObjPool.Get();
    }
}
