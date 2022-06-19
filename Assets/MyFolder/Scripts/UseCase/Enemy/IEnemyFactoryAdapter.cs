using System.Collections.Generic;
public interface IEnemyFactoryAdapter
{
    List<Player> CreatedEnemies { get; }
    void CreateEnemy(ReceivePacket packet);
    bool CheckCreatedEnemy(ReceivePacket packet);

    void DeleteEnemy(Player enemy);

}