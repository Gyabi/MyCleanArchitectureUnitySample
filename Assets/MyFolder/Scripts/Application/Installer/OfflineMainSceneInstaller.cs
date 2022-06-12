using UnityEngine;
using Zenject;

public class OfflineMainSceneInstaller : MonoInstaller
{
    [SerializeField]
    private MyPrefabFactory _prefabFactory;

    [SerializeField]
    private RandomMapMaker _randomMapMaker;

    [SerializeField]
    private BlockCommandView _blockCommandView;
    public override void InstallBindings()
    {
        // Mapユースケース
        #region MapUseCase
        Container.Bind<IMapUseCase>().To<MapUseCase>().AsSingle();
        Container.Bind<IMapCreateAdapter>().FromInstance(new MapCreateAdapter(_randomMapMaker)).AsCached();
        #endregion

        #region PlayerUseCase
        Container.Bind<IPlayerUseCase>().To<OfflinePlayerUseCase>().AsCached();
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
        #endregion
    }
}