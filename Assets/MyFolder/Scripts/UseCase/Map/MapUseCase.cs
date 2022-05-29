public class MapUseCase
{
    private readonly IMapCreateAdapter _mapCreateAdapter;

    public MapUseCase(IMapCreateAdapter mapCreateAdapter)
    {
        _mapCreateAdapter = mapCreateAdapter;
    }

    public void Init()
    {
        _mapCreateAdapter.CreateMap();
    }
}