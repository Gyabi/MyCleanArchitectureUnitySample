using UnityEngine;
using Zenject;

public class OnlineMainSceneInstaller : MonoInstaller
{
    [SerializeField]
    private MyPrefabFactory _prefabFactory;

    [SerializeField]
    private RandomMapMaker _randomMapMaker;

    [SerializeField]
    private BlockCommandView _blockCommandView;

    [SerializeField]
    private WebSocketClient _webSocketClient;
    public override void InstallBindings()
    {
        // Mapユースケース
        #region MapUseCase
        // UseCase本体
        Container.Bind<IMapUseCase>().To<MapUseCase>().AsSingle();
        // マップ生成関連
        Container.Bind<IMapCreateAdapter>().FromInstance(new MapCreateAdapter(_randomMapMaker)).AsCached();
        #endregion

        #region PlayerUseCase
        // UseCase本体
        Container.Bind<IPlayerUseCase>().To<OnlinePlayerUseCase>().AsCached();
        // Player生成
        Container.Bind<IPlayerFactoryAdapter>().FromInstance(new PlayerFactoryAdapter(_prefabFactory)).AsCached();
        // ブロック生成関連
        Container.Bind<IBlockModeController>().FromInstance(new BlockModeController(_blockCommandView)).AsCached();
        Container.Bind<IBlockCreateController>().FromInstance(new BlockCreateController(_blockCommandView)).AsCached();
        Container.Bind<IBlockDestroyController>().FromInstance(new BlockDestroyController(_blockCommandView)).AsCached();
        Container.Bind<IBlockCreateAdapter>().FromInstance(new BlockCreateAdapter(_prefabFactory)).AsCached();
        Container.Bind<IBlockDestroyAdapter>().FromInstance(new BlockDestroyAdapter(_prefabFactory)).AsCached();
        Container.Bind<IBlockCreatePresetner>().FromInstance(new BlockCreatePresenter(_blockCommandView)).AsCached();
        Container.Bind<IBlockResourcePresenter>().To<BlockResoucePresenter>().AsCached();
        // 通信関連
        Container.Bind<IPlayerServerSendAdapter>().FromInstance(new PlayerServerSendAdapter(_webSocketClient)).AsCached();
        // Container.Bind<IPlayerServerSendAdapter>().FromInstance(new PlayerServerSendAdapterTest()).AsCached();
        #endregion
    }
}