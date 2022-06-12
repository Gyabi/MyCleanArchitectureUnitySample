using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
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
        Container.Bind<IMapCreateAdapter>().FromInstance(new MapCreateAdapter(_randomMapMaker)).AsCached();

        #region PlayerUseCase
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
        Container.Bind<IPlayerServerSendAdapter>().FromInstance(new PlayerServerSendAdapterTest()).AsCached();
        #endregion
    }
}