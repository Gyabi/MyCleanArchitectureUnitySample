using UnityEngine;
using Zenject;

public class OfflineMainSceneStart : MonoBehaviour
{
    private MapUseCase _mapUseCase;
    private PlayerUseCase _playerUseCase;

    [Inject]
    private IMapCreateAdapter _mapCreateAdapter;
    [Inject]
    private IBlockModeController _blockModeController;
    [Inject]
    private IBlockResourcePresenter _blockResourcePresenter;
    [Inject]
    private IBlockDestroyAdapter _blockDestroyAdapter;
    [Inject]
    private IBlockCreateAdapter _blockCreateAdapter;
    [Inject]
    private IPlayerFactoryAdapter _playerFactoryAdapter;
    [Inject]
    private IBlockCreateController _blockCreateController;
    [Inject]
    private IBlockDestroyController _blockDestroyController;
    [Inject]
    private IBlockCreatePresetner _blockCreatePresetner;
    [Inject]
    private IPlayerServerSendAdapter _playerServerSendAdapter;
    void Start()
    {
        this._mapUseCase = new MapUseCase(_mapCreateAdapter);
        this._playerUseCase = new PlayerUseCase(_blockModeController, _blockResourcePresenter, _blockDestroyAdapter, _blockCreateAdapter, _playerFactoryAdapter, _blockCreateController, _blockDestroyController, _blockCreatePresetner, _playerServerSendAdapter);
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
