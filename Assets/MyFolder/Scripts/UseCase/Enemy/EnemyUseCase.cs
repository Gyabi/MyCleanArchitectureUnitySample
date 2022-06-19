using UniRx;
using Zenject;
public class EnemyUseCase : IEnemyUseCase
{
    #region DI Objects
    [Inject]
    private IEnemyFactoryAdapter _enemyFactoryAdapter;
    [Inject] 
    private IObjectTransformChangeAdapter _objectTransformChangeAdapter;
    [Inject]
    private IEnemyServerReceiveAdaper _enemyServerReceieveAdaper;
    #endregion

// メッセージ受信
// オブジェクト生成
// transform設定

    public void Init()
    {
        // メッセージ受信の通知を購読
        this._enemyServerReceieveAdaper.OnReceiveSubject.ObserveOnMainThread()
        .Subscribe(
            (ReceivePacket packet) =>
            {
                // 既に生成しているオブジェクトか判定
                if(this.CheckCreatedEnemy(packet))
                {
                    // プレイヤー生成
                    this._enemyFactoryAdapter.CreateEnemy(packet);
                }
                // 該当のオブジェクト情報を取得
                Player enemy = this._enemyFactoryAdapter.CreatedEnemies.Find(x => x._id == packet._id);
                
                this._objectTransformChangeAdapter.TransformChange(enemy, packet);
        });

        // 別クライアント接続終了時
        this._enemyServerReceieveAdaper.OnDisconnectionSubject.ObserveOnMainThread()
        .Subscribe(
            (int id) => 
            {
                // 該当のオブジェクト情報を取得
                Player enemy = this._enemyFactoryAdapter.CreatedEnemies.Find(x => x._id == id);
                // 削除
                this._enemyFactoryAdapter.DeleteEnemy(enemy);

                UnityEngine.Debug.Log("別クライアント接続終了");
            }
        );
    }

    public bool CheckCreatedEnemy(ReceivePacket packet)
    {
        return this._enemyFactoryAdapter.CheckCreatedEnemy(packet);
    }
}