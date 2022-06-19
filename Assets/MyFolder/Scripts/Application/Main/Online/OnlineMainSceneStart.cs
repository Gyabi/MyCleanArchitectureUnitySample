using UnityEngine;
using Zenject;

public class OnlineMainSceneStart : MonoBehaviour
{
    [Inject]
    private IMapUseCase _mapUseCase;
    [Inject]
    private IPlayerUseCase _playerUseCase;
    [Inject]
    private IEnemyUseCase _enemyUseCase;

    void Start()
    {
        // マップ作製
        _mapUseCase.Init();
        // プレイヤー作製、各動作購読
        _playerUseCase.Init();
        // エネミー作製、各動作購読
        _enemyUseCase.Init();
    }

    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        this._playerUseCase.FixedUpdate();
    }

}
