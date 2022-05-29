public class BlockCreateAdapter : IBlockCreateAdapter
{
    private MyPrefabFactory _prefabFactory;
    public BlockCreateAdapter(MyPrefabFactory prefabFactory)
    {
        this._prefabFactory = prefabFactory;
    }

    public void CreateBlock(BlockPoint point)
    {
        _prefabFactory.CreateBlock(point);
    }
}
