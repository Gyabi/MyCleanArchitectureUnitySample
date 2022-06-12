using Zenject;
public class MapUseCase : IMapUseCase
{
    [Inject]
    private readonly IMapCreateAdapter _mapCreateAdapter;

    // public MapUseCase(IMapCreateAdapter mapCreateAdapter)
    // {
    //     _mapCreateAdapter = mapCreateAdapter;
    // }

    public void Init()
    {
        _mapCreateAdapter.CreateMap();
    }
}