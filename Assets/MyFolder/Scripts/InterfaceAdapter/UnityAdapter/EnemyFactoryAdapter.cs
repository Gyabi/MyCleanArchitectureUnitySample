using System.Collections.Generic;
using UnityEngine;
public class EnemyFactoryAdapter : IEnemyFactoryAdapter
{
    private MyPrefabFactory _prefabFactory;

    public EnemyFactoryAdapter(MyPrefabFactory prefabFactory)
    {
        this._prefabFactory = prefabFactory;
    }

    private List<Player> _createdEnemies = new List<Player>();
    public List<Player> CreatedEnemies
    {
        get
        {
            return _createdEnemies;
        }
    }

    public void CreateEnemy(ReceivePacket packet)
    {
        var enemy = _prefabFactory.CreateEnemy();
        _createdEnemies.Add(new Player(enemy, packet._id));
    }

    public bool CheckCreatedEnemy(ReceivePacket packet)
    {
        return this.CreatedEnemies.Find(x => x._id == packet._id) == null;
    }

    public void DeleteEnemy(Player enemy)
    {
        GameObject.Destroy(enemy._obj.gameObject);
        this.CreatedEnemies.Remove(enemy);
    }
}