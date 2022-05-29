public class PlayerFactoryAdapter : IPlayerFactoryAdapter
{
    private MyPrefabFactory _prefabFactory;
    public PlayerFactoryAdapter(MyPrefabFactory prefabFactory)
    {
        this._prefabFactory = prefabFactory;
    }

    public void CreatePlayer()
    {
        _prefabFactory.CreatePlayer();
        
    }
}
