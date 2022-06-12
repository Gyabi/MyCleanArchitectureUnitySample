using UnityEngine;
public class PlayerFactoryAdapter : IPlayerFactoryAdapter
{
    private MyPrefabFactory _prefabFactory;
    public PlayerFactoryAdapter(MyPrefabFactory prefabFactory)
    {
        this._prefabFactory = prefabFactory;
    }

    public Player CreatePlayer()
    {
        GameObject obj = _prefabFactory.CreatePlayer();
        return new Player(obj);
    }
}
