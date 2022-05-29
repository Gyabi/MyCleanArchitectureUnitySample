using UnityEngine;

public class BlockDestroyAdapter : IBlockDestroyAdapter
{
    private MyPrefabFactory _prefabFactory;
    public BlockDestroyAdapter(MyPrefabFactory prefabFactory)
    {
        this._prefabFactory = prefabFactory;
    }
    public void DestroyBlock(GameObject block)
    {
        _prefabFactory.DestroyBlock(block);
    }
}
