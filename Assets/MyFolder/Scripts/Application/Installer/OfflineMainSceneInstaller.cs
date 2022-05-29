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
        Container.Bind<IMapCreateAdapter>().FromInstance(new MapCreateAdapter(_randomMapMaker)).AsCached();

        // playerユースケース
        Container.Bind<IPlayerFactoryAdapter>().FromInstance(new PlayerFactoryAdapter(_prefabFactory)).AsCached();
        Container.Bind<IBlockModeController>().FromInstance(new BlockModeController(_blockCommandView)).AsCached();
        Container.Bind<IBlockResourcePresenter>().To<BlockResoucePresenter>().AsCached();
        Container.Bind<IBlockDestroyAdapter>().FromInstance(new BlockDestroyAdapter(_prefabFactory)).AsCached();
        Container.Bind<IBlockCreateAdapter>().FromInstance(new BlockCreateAdapter(_prefabFactory)).AsCached();
        Container.Bind<IBlockCreateController>().FromInstance(new BlockCreateController(_blockCommandView)).AsCached();
        Container.Bind<IBlockDestroyController>().FromInstance(new BlockDestroyController(_blockCommandView)).AsCached();
        Container.Bind<IBlockCreatePresetner>().FromInstance(new BlockCreatePresenter(_blockCommandView)).AsCached();
    }
}