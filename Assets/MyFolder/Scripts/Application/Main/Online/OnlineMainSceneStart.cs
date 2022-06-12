using UnityEngine;
using Zenject;

public class OnlineMainSceneStart : MonoBehaviour
{
    [Inject]
    private IMapUseCase _mapUseCase;
    [Inject]
    private IPlayerUseCase _playerUseCase;

    void Start()
    {
        // マップ作製
        _mapUseCase.Init();
        // プレイヤー作製、各動作購読
        _playerUseCase.Init();
    }

    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        this._playerUseCase.FixedUpdate();
    }

}
