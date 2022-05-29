public class MapCreateAdapter : IMapCreateAdapter
{
    RandomMapMaker _randomMapMaker;

    public MapCreateAdapter(RandomMapMaker randomMapMaker)
    {
        _randomMapMaker = randomMapMaker;
    }
    void IMapCreateAdapter.CreateMap()
    {
        _randomMapMaker.CreateMap();
    }
}