using UnityEngine;
using Zenject;

public class OfflineMainSceneStart : MonoBehaviour
{
    private MapUseCase _mapUseCase;
    private PlayerUseCase _playerUseCase;
    [Inject]
    void InstallMapUseCase(IMapCreateAdapter mapCreateAdapter)
    {
        this._mapUseCase = new MapUseCase(mapCreateAdapter);
    }

    [Inject]
    void InstallPlayerUseCase(IBlockModeController blockModeConroller, IBlockResourcePresenter blockResourcePresenter, IBlockDestroyAdapter blockDestroyAdapter, IBlockCreateAdapter blockCreateAdapter, IPlayerFactoryAdapter playerFactoryAdapter, IBlockCreateController blockCreateController, IBlockDestroyController blockDestroyController, IBlockCreatePresetner blockCreatePresetner)
    {
        this._playerUseCase = new PlayerUseCase(blockModeConroller, blockResourcePresenter, blockDestroyAdapter, blockCreateAdapter, playerFactoryAdapter, blockCreateController, blockDestroyController, blockCreatePresetner);
    }
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

}
